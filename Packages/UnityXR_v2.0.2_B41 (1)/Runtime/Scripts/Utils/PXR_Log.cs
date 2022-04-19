// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

#if UNITY_ANDROID && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine;

namespace Unity.XR.PXR
{
    public class PLog
    {
        public static LogPriority logPriority = LogPriority.LogFatal;
        public enum LogPriority
        {
            LogVerbose = 2,
            LogDebug,
            LogInfo,
            LogWarn,
            LogError,
            LogFatal,
        }

        public static void v(string tag, string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (logPriority >= LogPriority.LogVerbose)
                Debug.Log(string.Format("{0} FrameID={1}>>>>>>{2}", tag, Time.frameCount, message));
#endif
        }

        public static void d(string tag, string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (logPriority >= LogPriority.LogDebug)
                Debug.Log(string.Format("{0} FrameID={1}>>>>>>{2}", tag, Time.frameCount, message));
#endif
        }

        public static void i(string tag, string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (logPriority >= LogPriority.LogInfo)
                Debug.Log(string.Format("{0} FrameID={1}>>>>>>{2}", tag, Time.frameCount, message));
#endif
        }

        public static void w(string tag, string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (logPriority >= LogPriority.LogWarn)
                Debug.LogWarning(string.Format("{0} FrameID={1}>>>>>>{2}", tag, Time.frameCount, message));
#endif
        }

        public static void e(string tag, string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (logPriority >= LogPriority.LogError)
                Debug.LogError(string.Format("{0} FrameID={1}>>>>>>{2}", tag, Time.frameCount, message));
#endif
        }

        public static void f(string tag, string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (logPriority >= LogPriority.LogFatal)
                Debug.LogError(string.Format("{0} FrameID={1}>>>>>>{2}", tag, Time.frameCount, message));
#endif
        }
    }
}