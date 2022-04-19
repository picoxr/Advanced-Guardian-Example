// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

#if UNITY_EDITOR_WIN && UNITY_ANDROID
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class PXR_BuildAndRunEW : EditorWindow
{
#if UNITY_EDITOR&& UNITY_ANDROID
    
    static string applicationIdentifier;
    static string productName;
    static string gradleTempExport;
    static string gradleExport;
    static PXR_DirectorySyncer.CancellationTokenSource syncCancelToken;
    static Process gradleBuildProcess;
    static Thread buildThread;
    static bool? apkOutputSuccessful;

#if UNITY_2018_4_OR_NEWER
    [MenuItem("PXR_SDK/Build Tool/Build And Run")]
    static void BuildAndRun()
    {
        if (!PXR_ADBTool.GetInstance().CheckADBDevices())
        {
            return;
        }

        apkOutputSuccessful = null;
        syncCancelToken = null;
        gradleBuildProcess = null;

        gradleTempExport = Path.Combine(Path.Combine(Application.dataPath, "../Temp"), "PXRGradleTempExport");
        gradleExport = Path.Combine(Path.Combine(Application.dataPath, "../Temp"), "PXRGradleExport");
        if (!Directory.Exists(gradleExport))
        {
            Directory.CreateDirectory(gradleExport);
        }

        var buildResult = UnityBuildPlayer();

        if (buildResult.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            applicationIdentifier = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
#if UNITY_2019_3_OR_NEWER
            productName = "launcher";
#else
            productName = Application.productName;
#endif

            BuildRun();
        }
    }

    private static UnityEditor.Build.Reporting.BuildReport UnityBuildPlayer()
    {
#if UNITY_2020_1_OR_NEWER || UNITY_2019_4_OR_NEWER
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
#endif
        PXR_BuildToolManager.GetScenesEnabled();
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = PXR_BuildToolManager.buildSceneNameList.ToArray(),
            locationPathName = gradleTempExport,
            target = BuildTarget.Android,
            options = BuildOptions.Development
                      | BuildOptions.AllowDebugging
#if !UNITY_2020_1_OR_NEWER && !UNITY_2019_4_OR_NEWER
                      | BuildOptions.AcceptExternalModificationsToPlayer
#endif
        };

        var buildResult = BuildPipeline.BuildPlayer(buildPlayerOptions);
        return buildResult;
    }
#endif

    private static void BuildRun()
    {
        if (ProcessGradleProject())
        {
            if (BuildGradleProject())
            {
                DeployAPK();
            }
        }
    }

    private static bool BuildGradleProject()
    {
        gradleBuildProcess = new Process();
        string arguments = "-Xmx4096m -classpath \"" + PXR_ADBTool.GetInstance().GetGradlePath() +
            "\" org.gradle.launcher.GradleMain assembleDebug -x validateSigningDebug --profile";
#if UNITY_2019_3_OR_NEWER
        var gradleProjectPath = gradleExport;
#else
        var gradleProjectPath = Path.Combine(gradleExport, productName);
#endif

        var processInfo = new ProcessStartInfo
        {
            WorkingDirectory = gradleProjectPath,
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = PXR_ADBTool.GetInstance().GetJDKPath(),
            Arguments = arguments,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
        };

        gradleBuildProcess.StartInfo = processInfo;
        gradleBuildProcess.EnableRaisingEvents = true;

        DateTime gradleStartTime = System.DateTime.Now;
        DateTime gradleEndTime = System.DateTime.MinValue;

        gradleBuildProcess.Exited += (s, e) =>
        {
            UnityEngine.Debug.Log("PXRLog Gradle: Exited");
        };

        gradleBuildProcess.OutputDataReceived += new DataReceivedEventHandler(
            (s, e) =>
            {
                if (e != null && e.Data != null &&
                    e.Data.Length != 0 &&
                    (e.Data.Contains("BUILD") || e.Data.StartsWith("See the profiling report at:")))
                {
                    UnityEngine.Debug.LogFormat("PXRLog Gradle: {0}", e.Data);
                    if (e.Data.Contains("SUCCESSFUL"))
                    {
                        UnityEngine.Debug.LogFormat("PXRLog APK Build Completed: {0}",
                            Path.Combine(Path.Combine(gradleProjectPath, "build\\outputs\\apk\\debug"), productName + "-debug.apk").Replace("/", "\\"));
                        if (!apkOutputSuccessful.HasValue)
                        {
                            apkOutputSuccessful = true;
                        }
                        gradleEndTime = System.DateTime.Now;
                    }
                    else if (e.Data.Contains("FAILED"))
                    {
                        apkOutputSuccessful = false;
                    }
                }
            }
        );

        gradleBuildProcess.ErrorDataReceived += new DataReceivedEventHandler(
            (s, e) =>
            {
                if (e != null && e.Data != null &&
                    e.Data.Length != 0)
                {
                    UnityEngine.Debug.LogErrorFormat("Gradle: {0}", e.Data);
                }
                apkOutputSuccessful = false;
            }
        );

        gradleBuildProcess.Start();
        gradleBuildProcess.BeginOutputReadLine();
        gradleBuildProcess.WaitForExit();
        
        Stopwatch timeout = new Stopwatch();
        timeout.Start();
        while (apkOutputSuccessful == null)
        {
            if (timeout.ElapsedMilliseconds > 5000)
            {
                UnityEngine.Debug.LogError("PXRLog Gradle has exited unexpectedly.");
                apkOutputSuccessful = false;
            }
            Thread.Sleep(100);
        }
        
        return apkOutputSuccessful.HasValue && apkOutputSuccessful.Value;
    }

    private static bool ProcessGradleProject()
    {
        try
        {
            var ps = System.Text.RegularExpressions.Regex.Escape("" + Path.DirectorySeparatorChar);
             
            var ignorePattern = string.Format("^([^{0}]+{0})?(\\.gradle|build){0}", ps);

            var syncer = new PXR_DirectorySyncer(gradleTempExport,
                gradleExport, ignorePattern);

            syncCancelToken = new PXR_DirectorySyncer.CancellationTokenSource();
            var syncResult = syncer.Synchronize(syncCancelToken.Token);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("PXRLog Processing gradle project failed with exception: " +
                                  e.Message);
            return false;
        }

        if (syncCancelToken.IsCancellationRequested)
        {
            return false;
        }
        return true;
    }

    public static bool DeployAPK()
    {
        if (!PXR_ADBTool.GetInstance().IsReady())
        {
            return false;
        }

        string gradleExportFolder = Path.Combine(Path.Combine(gradleExport, productName), "build\\outputs\\apk\\debug");
        gradleExportFolder = gradleExportFolder.Replace("/", "\\");
        if (!Directory.Exists(gradleExportFolder))
        {
            return false;
        }
        
        var apkPathLocal = Path.Combine(gradleExportFolder, productName + "-debug.apk");
        if (!File.Exists(apkPathLocal))
        {
            return false;
        }

        string output, error;
        
        string[] mkdirCommand = { "-d shell", "mkdir -p", "/data/local/tmp" };
        if (PXR_ADBTool.GetInstance().RunCommand(mkdirCommand, null, out output, out error) != 0) return false;
        
        var timerStart = DateTime.Now;
        string[] pushCommand = { "-d push", "\"" + apkPathLocal + "\"", "/data/local/tmp" };
        if (PXR_ADBTool.GetInstance().RunCommand(pushCommand, null, out output, out error) != 0) return false;
        
        TimeSpan pushTime = System.DateTime.Now - timerStart;
        bool trivialPush = pushTime.TotalSeconds < 4.0f;
        long? apkSize = (trivialPush ? (long?)null : new System.IO.FileInfo(apkPathLocal).Length);
        double? transferSpeed = (apkSize / pushTime.TotalSeconds) / 1048576;
        bool informLog = transferSpeed.HasValue && transferSpeed.Value < 25.0f;
        
        string apkPath = "/data/local/tmp" + "/" + productName + "-debug.apk";
        apkPath = apkPath.Replace(" ", "\\ ");
        string[] installCommand = { "-d shell", "pm install -r", apkPath };

        timerStart = DateTime.Now;
        if (PXR_ADBTool.GetInstance().RunCommand(installCommand, null, out output, out error) != 0) return false;
        TimeSpan installTime = System.DateTime.Now - timerStart;
        
#if UNITY_2019_3_OR_NEWER
        string playerActivityName = "\"" + applicationIdentifier + "/com.unity3d.player.UnityPlayerActivity\"";
#else
        string playerActivityName = "\"" + applicationIdentifier + "/" + applicationIdentifier + ".UnityPlayerActivity\"";
#endif
        string[] appStartCommand = { "-d shell", "am start -a android.intent.action.MAIN -c android.intent.category.LAUNCHER -S -f 0x10200000 -n", playerActivityName };
        if (PXR_ADBTool.GetInstance().RunCommand(appStartCommand, null, out output, out error) != 0) return false;
        UnityEngine.Debug.Log("PXRLog Application Start Success");
        
        if (informLog)
        {
            return true;
        }
        return false;
    }

#endif
}
#endif