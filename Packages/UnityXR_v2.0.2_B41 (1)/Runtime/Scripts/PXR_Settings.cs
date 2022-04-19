// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using System;
using UnityEngine;
using UnityEngine.XR.Management;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Unity.XR.PXR
{
    [Serializable]
    [XRConfigurationData("PicoXR", "Unity.XR.PXR.Settings")]
    public class PXR_Settings : ScriptableObject
    {
        public enum StereoRenderingModeAndroid
        {
            MultiPass,
            Multiview
        }

        public enum SystemDisplayFrequency
        {
            Default,
            RefreshRate72,
            RefreshRate90,
        }

        [SerializeField, Tooltip("Set the Stereo Rendering Method")]
        public StereoRenderingModeAndroid stereoRenderingModeAndroid;

        [SerializeField, Tooltip("Set the system display frequency")]
        public SystemDisplayFrequency systemDisplayFrequency;

        public ushort GetStereoRenderingMode()
        {
            return (ushort)stereoRenderingModeAndroid;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
		public static PXR_Settings settings;
		public void Awake()
		{
            settings = this;
		}
#endif
    }
}
