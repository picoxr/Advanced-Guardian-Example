// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Management;
using UnityEngine.XR;
using AOT;

#if UNITY_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
using Unity.XR.PXR.Input;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Unity.XR.PXR
{
#if UNITY_INPUT_SYSTEM
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    static class InputLayoutLoader
    {
        static InputLayoutLoader()
        {
            RegisterInputLayouts();
        }

        public static void RegisterInputLayouts()
        {
            InputSystem.RegisterLayout<PXR_HMD>(matches: new InputDeviceMatcher().WithInterface(XRUtilities.InterfaceMatchAnyVersion).WithProduct("^(Pico Neo)|^(Pico G)"));
            InputSystem.RegisterLayout<PXR_Controller>(matches: new InputDeviceMatcher().WithInterface(XRUtilities.InterfaceMatchAnyVersion).WithProduct(@"^(PicoXR Controller)"));
        }
    }
#endif

    public class PXR_Loader : XRLoaderHelper
#if UNITY_EDITOR
    , IXRLoaderPreInit
#endif
    {
        private static List<XRDisplaySubsystemDescriptor> displaySubsystemDescriptors = new List<XRDisplaySubsystemDescriptor>();
        private static List<XRInputSubsystemDescriptor> inputSubsystemDescriptors = new List<XRInputSubsystemDescriptor>();
        private static List<PXR_PassThroughDescriptor> cameraSubsystemDescriptors = new List<PXR_PassThroughDescriptor>();

        public delegate Quaternion ConvertRotationWith2VectorDelegate(Vector3 from, Vector3 to);

        public XRDisplaySubsystem displaySubsystem
        {
            get
            {
                return GetLoadedSubsystem<XRDisplaySubsystem>();
            }
        }

        public XRInputSubsystem inputSubsystem
        {
            get
            {
                return GetLoadedSubsystem<XRInputSubsystem>();
            }
        }

        public PXR_PassThroughSystem passThroughSystem
        {
            get
            {
                return GetLoadedSubsystem<PXR_PassThroughSystem>();
            }
        }

        public override bool Initialize()
        {
#if UNITY_INPUT_SYSTEM
            InputLayoutLoader.RegisterInputLayouts();
#endif
            PXR_Settings settings = GetSettings();
            float rate = -1.0f;
            switch (settings.systemDisplayFrequency)
            {
                case PXR_Settings.SystemDisplayFrequency.Default:
                    {
                        rate = 0.0f;
                        break;
                    }
                case PXR_Settings.SystemDisplayFrequency.RefreshRate72:
                    {
                        rate = 72.0f;
                        break;
                    }
                case PXR_Settings.SystemDisplayFrequency.RefreshRate90:
                    {
                        rate = 90.0f;
                        break;
                    }
            }
            if (settings != null)
            {
                UserDefinedSettings userDefinedSettings = new UserDefinedSettings
                {
                    stereoRenderingMode = (ushort)settings.GetStereoRenderingMode(),
                    colorSpace = (ushort)((QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0),
                    useContentProtect = PXR_ProjectSetting.GetProjectConfig().useContentProtect,
                    systemDisplayFrequency = rate,
                };

                PXR_Plugin.System.UPxr_Construct(ConvertRotationWith2Vector);
                PXR_Plugin.System.UPxr_SetUserDefinedSettings(userDefinedSettings);
            }

            CreateSubsystem<XRDisplaySubsystemDescriptor, XRDisplaySubsystem>(displaySubsystemDescriptors, "PicoXR Display");
            CreateSubsystem<XRInputSubsystemDescriptor, XRInputSubsystem>(inputSubsystemDescriptors, "PicoXR Input");
            CreateSubsystem<PXR_PassThroughDescriptor, PXR_PassThroughSystem>(cameraSubsystemDescriptors, "PicoXR Camera");
        
            if (displaySubsystem == null || inputSubsystem == null)
            {
                Debug.LogError("PXRLog Unable to start Pico XR Plugin.");
            }

            if (displaySubsystem == null)
            {
                Debug.LogError("PXRLog Failed to load display subsystem.");
            }

            if (inputSubsystem == null)
            {
                Debug.LogError("PXRLog Failed to load input subsystem.");
            }

            if (passThroughSystem == null)
            {
                Debug.LogError("PXRLog Failed to load passThrough system.");
            }
 
            return displaySubsystem != null;
        }

        public override bool Start()
        {
            StartSubsystem<XRDisplaySubsystem>();
            StartSubsystem<XRInputSubsystem>();

            return true;
        }

        public override bool Stop()
        {
            StopSubsystem<XRDisplaySubsystem>();
            StopSubsystem<XRInputSubsystem>();

            return true;
        }

        public override bool Deinitialize()
        {
            DestroySubsystem<XRDisplaySubsystem>();
            DestroySubsystem<XRInputSubsystem>();
            DestroySubsystem<PXR_PassThroughSystem>();

            return true;
        }

        [MonoPInvokeCallback(typeof(ConvertRotationWith2VectorDelegate))]
        static Quaternion ConvertRotationWith2Vector(Vector3 from, Vector3 to)
        {
            return Quaternion.FromToRotation(from, to);
        }

        public PXR_Settings GetSettings()
        {
            PXR_Settings settings = null;
#if UNITY_EDITOR
            UnityEditor.EditorBuildSettings.TryGetConfigObject<PXR_Settings>("Unity.XR.PXR.Settings", out settings);
#else
            settings = PXR_Settings.settings;
#endif
            return settings;
        }

#if UNITY_EDITOR
        public string GetPreInitLibraryName(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup)
        {
            return "PxrPlatform";
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void RuntimeLoadPicoPlugin()
        {
            PXR_Plugin.System.UPxr_LoadPicoPlugin();
            string version = "UnityXR_" + PXR_Plugin.System.UPxr_GetSDKVersion();
            PXR_Plugin.System.UPxr_SetConfigString( ConfigType.EngineVersion, version );
        }
#endif
    }
}
