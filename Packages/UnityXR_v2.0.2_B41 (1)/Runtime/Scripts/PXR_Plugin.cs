// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using System.ComponentModel;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.XR.PXR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UserDefinedSettings
    {
        public ushort stereoRenderingMode;
        public ushort colorSpace;
        public int antiAliasing;
        public int renderTextureDepth;
        public bool useContentProtect;
        public float systemDisplayFrequency;
    }

    public enum RenderEvent
    {
        CreateTexture,
        DeleteTexture,
        UpdateTexture
    }

    public enum ResUtilsType
    {
        TypeTextSize,
        TypeColor,
        TypeText,
        TypeFont,
        TypeValue,
        TypeDrawable,
        TypeObject,
        TypeObjectArray,
    }

    public enum GraphicsAPI
    {
        OpenGLES,
        Vulkan
    };

    public enum EyeType
    {
        EyeLeft,
        EyeRight,
        EyeBoth
    };

    public enum ConfigType
    {
        RenderTextureWidth,
        RenderTextureHeight,
        ShowFps,
        RuntimeLogLevel,
        PluginLogLevel,
        UnityLogLevel,
        UnrealLogLevel,
        NativeLogLevel,
        TargetFrameRate,
        NeckModelX,
        NeckModelY,
        NeckModelZ,
        DisplayRefreshRate,
        Ability6Dof,
        DeviceModel,
        PhysicalIPD,
        ToDelaSensorY,
        SystemDisplayRate,
        FoveationSubsampledEnabled,
        TrackingOriginHeight,
        EngineVersion,
        UnrealOpenglNoError,
        EnableCPT,
        MRCTextureID,
        RenderFPS,
        AntiAliasingLevelRecommended,
        PxrMRCTextureID2,
        PxrSetSurfaceView,
        PxrAPIVersion
    };

    public enum FoveationLevel
    {
        None = -1,
        Low,
        Med,
        High,
        TopHigh
    }

    public enum BoundaryType
    {
        OuterBoundary,
        PlayArea
    }

    public enum BoundaryTrackingNode
    {
        HandLeft,
        HandRight,
        Head
    }

    public enum ResetSensorOption
    {
        ResetPosition,
        ResetRotation,
        ResetRotationYOnly,
        ResetAll
    };

    public enum SystemInfoEnum
    {
        ELECTRIC_QUANTITY,
        PUI_VERSION,
        EQUIPMENT_MODEL,
        EQUIPMENT_SN,
        CUSTOMER_SN,
        INTERNAL_STORAGE_SPACE_OF_THE_DEVICE,
        DEVICE_BLUETOOTH_STATUS,
        BLUETOOTH_NAME_CONNECTED,
        BLUETOOTH_MAC_ADDRESS,
        DEVICE_WIFI_STATUS,
        WIFI_NAME_CONNECTED,
        WLAN_MAC_ADDRESS,
        DEVICE_IP,
        CHARGING_STATUS
    }

    public enum DeviceControlEnum
    {
        DEVICE_CONTROL_REBOOT,
        DEVICE_CONTROL_SHUTDOWN
    }

    public enum PackageControlEnum
    {
        PACKAGE_SILENCE_INSTALL,
        PACKAGE_SILENCE_UNINSTALL
    }

    public enum SwitchEnum
    {
        S_ON,
        S_OFF
    }

    public enum HomeEventEnum
    {
        SINGLE_CLICK,
        DOUBLE_CLICK
    }

    public enum HomeFunctionEnum
    {
        VALUE_HOME_GO_TO_SETTING = 0,
        VALUE_HOME_RECENTER = 2,
        VALUE_HOME_DISABLE = 4,
        VALUE_HOME_GO_TO_HOME = 5
    }

    public enum ScreenOffDelayTimeEnum
    {
        THREE = 3,
        TEN = 10,
        THIRTY = 30,
        SIXTY = 60,
        THREE_HUNDRED = 300,
        SIX_HUNDRED = 600,
        NEVER = -1
    }

    public enum SleepDelayTimeEnum
    {
        FIFTEEN = 15,
        THIRTY = 30,
        SIXTY = 60,
        THREE_HUNDRED = 300,
        SIX_HUNDRED = 600,
        ONE_THOUSAND_AND_EIGHT_HUNDRED = 1800,
        NEVER = -1
    }

    public enum SystemFunctionSwitchEnum
    {
        SFS_USB,
        SFS_AUTOSLEEP,
        SFS_SCREENON_CHARGING,
        SFS_OTG_CHARGING,
        SFS_RETURN_MENU_IN_2DMODE,
        SFS_COMBINATION_KEY,
        SFS_CALIBRATION_WITH_POWER_ON,
        SFS_SYSTEM_UPDATE,
        SFS_CAST_SERVICE,
        SFS_EYE_PROTECTION,
        SFS_SECURITY_ZONE_PERMANENTLY,
        SFS_GLOBAL_CALIBRATION,
        SFS_Auto_Calibration,
        SFS_USB_BOOT,
        SFS_VOLUME_UI,
        SFS_CONTROLLER_UI,
        SFS_NAVGATION_SWITCH,
        SFS_SHORTCUT_SHOW_RECORD_UI,
        SFS_SHORTCUT_SHOW_FIT_UI,
        SFS_SHORTCUT_SHOW_CAST_UI,
        SFS_SHORTCUT_SHOW_CAPTURE_UI,
        SFS_STOP_MEM_INFO_SERVICE,
        SFS_USB_FORCE_HOST,

    }

    public enum USBConfigModeEnum
    {
        MTP,
        CHARGE
    }

    public enum PxrLayerCreateFlags
    {
        PxrLayerFlagAndroidSurface = 1 << 0,
        PxrLayerFlagProtectedContent = 1 << 1,
        PxrLayerFlagStaticImage = 1 << 2,
        PxrLayerFlagUseExternalImages = 1 << 4,
    }

    public enum PxrLayerSubmitFlags
    {
        PxrLayerFlagNoCompositionDepthTesting = 1 << 3,
        PxrLayerFlagUseExternalHeadPose = 1 << 5,
        PxrLayerFlagLayerPoseNotInTrackingSpace = 1 << 6,
        PxrLayerFlagHeadLocked = 1 << 7,
        PxrLayerFlagUseExternalImageIndex = 1 << 8,
    }

    public struct FoveationParams
    {
        public float foveationGainX;
        public float foveationGainY;
        public float foveationArea;
        public float foveationMinimum;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EyeTrackingGazeRay
    {
        public Vector3 direction;
        public bool isValid;
        public Vector3 origin;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrSensorState
    {
        public int status;
        public PxrPosef pose;
        public PxrVector3f angularVelocity;
        public PxrVector3f linearVelocity;
        public PxrVector3f angularAcceleration;
        public PxrVector3f linearAcceleration;
        public UInt64 poseTimeStampNs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrSensorState2
    {
        public int status;
        public PxrPosef pose;
        public PxrPosef globalPose;
        public PxrVector3f angularVelocity;
        public PxrVector3f linearVelocity;
        public PxrVector3f angularAcceleration;
        public PxrVector3f linearAcceleration;
        public UInt64 poseTimeStampNs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrControllerTracking
    {
        public PxrSensorState localControllerPose;
        public PxrSensorState globalControllerPose;
    }

    public enum PxrControllerType
    {
        PxrInputG2 = 3,
        PxrInputNeo2 = 4,
        PxrInputNeo3 = 5
    }

    public enum PxrControllerDof
    {
        PxrController3Dof,
        PxrController6Dof
    }

    public enum PxrControllerBond
    {
        PxrControllerIsBond,
        PxrControllerUnBond
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrControllerCapability
    {
        public PxrControllerType type;
        public PxrControllerDof inputDof;
        public PxrControllerBond inputBond;
        public UInt64 Abilities;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerParam
    {
        public int layerId;
        public PXR_OverLay.OverlayShape layerShape;
        public PXR_OverLay.OverlayType layerType;
        public PXR_OverLay.LayerLayout layerLayout;
        public UInt64 format;
        public UInt32 width;
        public UInt32 height;
        public UInt32 sampleCount;
        public UInt32 faceCount;
        public UInt32 arraySize;
        public UInt32 mipmapCount;
        public UInt32 layerFlags;
        public UInt32 externalImageCount;
        public IntPtr leftExternalImages;
        public IntPtr rightExternalImages;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrVector4f
    {
        public float x;
        public float y;
        public float z;
        public float w;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrVector3f
    {
        public float x;
        public float y;
        public float z;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrBoundaryTriggerInfo
    {
        public bool isTriggering;
        public float closestDistance;
        public PxrVector3f closestPoint;
        public PxrVector3f closestPointNormal;
        public bool valid;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrPosef
    {
        public PxrVector4f orientation;
        public PxrVector3f position;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrRecti
    {
        public int x;
        public int y;
        public int width;
        public int height;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerHeader
    {
        public int layerId;
        public UInt32 layerFlags;
        public float colorScaleX;
        public float colorScaleY;
        public float colorScaleZ;
        public float colorScaleW;
        public float colorBiasX;
        public float colorBiasY;
        public float colorBiasZ;
        public float colorBiasW;
        public int compositionDepth;
        public int sensorFrameIndex;
        public int imageIndex;
        public PxrPosef headPose;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerQuad
    {
        public PxrLayerHeader header;
        public PxrPosef pose;
        public float width;
        public float height;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerCylinder
    {
        public PxrLayerHeader header;
        public PxrPosef pose;
        public float radius;
        public float centralAngle;
        public float height;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerEquirect
    {
        public PxrLayerHeader header;
        public PxrPosef pose;
        public float radius;
        public float centralHorizontalAngle;
        public float upperVerticalAngle;
        public float lowerVerticalAngle;
    };

    public static class PXR_Plugin
    {
        private const string PXR_SDK_Version = "2.0.2.4";
        private const string PXR_PLATFORM_DLL = "PxrPlatform";
        public const string PXR_API_DLL = "pxr_api";

        #region DLLImports
        //PassThrough
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraStart();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraStop();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraDestroy();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Pxr_CameraGetRenderEventFunc();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_CameraSetRenderEventPending();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_CameraWaitForRenderEvent();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraUpdateFrame(int eye);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraCreateTexturesMainThread();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraDeleteTexturesMainThread();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraUpdateTexturesMainThread();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetFoveationLevelEnable(int enable);

        //System
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_LoadPlugin();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_UnloadPlugin();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetHomeKey();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_InitHomeKey();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetUserDefinedSettings(UserDefinedSettings settings);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_Construct(PXR_Loader.ConvertRotationWith2VectorDelegate fromToRotation);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Pxr_RefreshRateChanged();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetSensorStatus();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_GetLayerImagePtr(int layerId, EyeType eye, int imageIndex, ref IntPtr image);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_CreateLayerParam(PxrLayerParam layerParam);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_DestroyLayerByRender(int layerId);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetGraphicOption(GraphicsAPI option);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_CreateLayer(IntPtr layerParam);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerNextImageIndex(int layerId, ref int imageIndex);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerImageCount(int layerId, EyeType eye, ref UInt32 imageCount);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerImage(int layerId, EyeType eye, int imageIndex, ref UInt64 image);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerAndroidSurface(int layerId, EyeType eye, ref IntPtr androidSurface);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_DestroyLayer(int layerId);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayer(IntPtr layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerQuad(PxrLayerQuad layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerCylinder(PxrLayerCylinder layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerEquirect(PxrLayerEquirect layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern FoveationLevel Pxr_GetFoveationLevel();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetFoveationParams(FoveationParams foveationParams);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetFrustum(EyeType eye, float fovLeft, float fovRight, float fovUp, float fovDown, float near, float far);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetFrustum(EyeType eye, ref float fovLeft, ref float fovRight, ref float fovUp, ref float fovDown, ref float near, ref float far);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetConfigFloat(ConfigType configIndex, ref float value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetConfigInt(ConfigType configIndex, ref int value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigInt(ConfigType configSetIndex, int configSetData);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigString(ConfigType configSetIndex, string configSetData);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigUint64(ConfigType configSetIndex, UInt64 configSetData);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_ResetSensor(ResetSensorOption option);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetSensorLostCustomMode(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetSensorLostCMST(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetDisplayRefreshRatesAvailable(ref int configCount, ref IntPtr configArray);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetDisplayRefreshRate(float refreshRate);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetDialogState();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetPredictedDisplayTime(ref double predictedDisplayTime);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_SetExtraLatencyMode(int mode);

        //Tracking Sensor
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetPredictedMainSensorState2(double predictTimeMs, ref PxrSensorState2 sensorState, ref int sensorFrameIndex);

        //Controller
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetControllerOriginOffset(int controllerID, Vector3 offset);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetControllerTrackingState(UInt32 deviceID, double predictTime, float[] headSensorData, ref PxrControllerTracking tracking);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerMainInputHandle(UInt32 deviceID);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetControllerMainInputHandle(ref int deviceID);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerVibration(UInt32 deviceID, float strength, int time);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetControllerCapabilities(UInt32 deviceID, ref PxrControllerCapability capability);

        //Large Space
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetLargeSpaceStatus(bool value);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetLargeSpaceEnable(bool value);



        //Boundary
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetSeeThroughState();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_TestNodeIsInBoundary(BoundaryTrackingNode node, bool isPlayArea, ref PxrBoundaryTriggerInfo info);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_TestPointIsInBoundary(ref PxrVector3f point, bool isPlayArea, ref PxrBoundaryTriggerInfo info);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetBoundaryGeometry(bool isPlayArea, UInt32 pointsCountInput, ref UInt32 pointsCountOutput, PxrVector3f[] outPoints);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetBoundaryDimensions(bool isPlayArea, out PxrVector3f dimension);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetBoundaryConfigured();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetBoundaryEnabled();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetBoundaryVisible(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetSeeThroughBackground(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetBoundaryVisible();

        #endregion

        public static class System
        {
            public static bool UPxr_LoadPicoPlugin()
            {
                PLog.d(TAG, "UPxr_LoadPicoPlugin");
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_LoadPlugin();
#else  
                return false;
#endif
            }

            public static void UPxr_UnloadPicoPlugin()
            {
                PLog.d(TAG, "UPxr_UnloadPicoPlugin");
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_UnloadPlugin();
#endif
            }

            public static string UPxr_GetSDKVersion()
            {
                return PXR_SDK_Version;
            }

            public static float UPxr_GetSystemDisplayFrequency()
            {
                return UPxr_GetConfigFloat(ConfigType.SystemDisplayRate);
            }

            public static double UPxr_GetPredictedDisplayTime()
            {
                PLog.i(TAG, "UPxr_GetPredictedDisplayTime()");
                double predictedDisplayTime = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetPredictedDisplayTime(ref predictedDisplayTime);
#endif
                return predictedDisplayTime;
            }

            public static bool UPxr_SetExtraLatencyMode(int mode)
            {
                PLog.i(TAG, "UPxr_SetExtraLatencyMode()");
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetExtraLatencyMode(mode);
#endif
                return false;
            }

            public static void UPxr_SetUserDefinedSettings(UserDefinedSettings settings)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetUserDefinedSettings(settings);
#endif
            }

            public static void UPxr_Construct(PXR_Loader.ConvertRotationWith2VectorDelegate fromToRotation)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_Construct(fromToRotation);
#endif
            }

            public static bool UPxr_GetHomeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetHomeKey();
#endif
                return false;
            }

            public static void UPxr_InitHomeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_InitHomeKey();
#endif
            }

            private const string TAG = "[PXR_Plugin/System]";
#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            private static AndroidJavaClass sysActivity = new AndroidJavaClass("com.psmart.aosoperation.SysActivity");
            private static AndroidJavaClass batteryReceiver = new AndroidJavaClass("com.psmart.aosoperation.BatteryReceiver");
            private static AndroidJavaClass audioReceiver = new AndroidJavaClass("com.psmart.aosoperation.AudioReceiver");
#endif
            public static string UPxr_GetDeviceMode()
            {
                string devicemode = "";
#if UNITY_ANDROID && !UNITY_EDITOR
            devicemode = SystemInfo.deviceModel;
#endif
                return devicemode;
            }

            public static float UPxr_GetConfigFloat(ConfigType type)
            {
                PLog.i(TAG, "UPxr_GetConfigFloat()");
                float value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetConfigFloat(type, ref value);
#endif
                return value;
            }

            public static int UPxr_GetConfigInt(ConfigType type)
            {
                PLog.i(TAG, "UPxr_GetConfigInt()");
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetConfigInt(type, ref value);
#endif
                return value;
            }

            public static int UPxr_SetConfigInt(ConfigType configSetIndex, int configSetData)
            {
                PLog.i(TAG, "UPxr_SetConfigInt()");
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetConfigInt(configSetIndex, configSetData);
#else
                return 0;
#endif
            }

            public static int UPxr_SetConfigString(ConfigType configSetIndex, string configSetData)
            {
                PLog.i(TAG, "UPxr_SetConfigString()");
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetConfigString(configSetIndex, configSetData);
#else
                return 0;
#endif
            }

            public static int UPxr_SetSystemDisplayFrequency(float rate)
            {
                PLog.i(TAG, "UPxr_SetDisplayRefreshRate()");
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetDisplayRefreshRate(rate);
#else
                return 0;
#endif
            }

            public static bool UPxr_InitAudioDevice()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                if (sysActivity != null)
                {
                    sysActivity.CallStatic("pxr_InitAudioDevice", currentActivity); 
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_InitAudioDevice Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_StartBatteryReceiver(string objName)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                batteryReceiver.CallStatic("pxr_StartReceiver", currentActivity, objName);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StartBatteryReceiver Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_StopBatteryReceiver()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                batteryReceiver.CallStatic("pxr_StopReceiver", currentActivity);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StopBatteryReceiver Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_SetBrightness(int brightness)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_SetScreen_Brightness", brightness, currentActivity);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_SetBrightness Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static int UPxr_GetCurrentBrightness()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            int currentlight = 0;
            try
            {
                currentlight = sysActivity.CallStatic<int>("pxr_GetScreen_Brightness", currentActivity);
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_GetCurrentBrightness Error :" + e.ToString());
            }
            return currentlight;
#else
                return 0;
#endif
            }

            public static int[] UPxr_GetScreenBrightnessLevel()
            {
                int[] currentlight = { 0 };
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                currentlight = sysActivity.CallStatic<int[]>("getScreenBrightnessLevel");
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_GetScreenBrightnessLevel Error :" + e.ToString());
            }
#endif
                return currentlight;
            }

            public static void UPxr_SetScreenBrightnessLevel(int vrBrightness, int level)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("setScreenBrightnessLevel",vrBrightness,level);
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_SetScreenBrightnessLevel Error :" + e.ToString());
            }
#endif
            }

            public static bool UPxr_StartAudioReceiver(string startreceivre)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                audioReceiver.CallStatic("pxr_StartReceiver", currentActivity, startreceivre);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StartAudioReceiver Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_StopAudioReceiver()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                audioReceiver.CallStatic("pxr_StopReceiver", currentActivity);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StopAudioReceiver Error :" + e.ToString());
                return false;
            }

#else
                return true;
#endif
            }

            public static int UPxr_GetMaxVolumeNumber()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            int maxvolm = 0;
            try
            {  
                maxvolm = sysActivity.CallStatic<int>("pxr_GetMaxAudionumber");
            }
            catch (Exception e)
            {
                PLog.e(TAG,"UPxr_GetMaxVolumeNumber Error :" + e.ToString());
            }
            return maxvolm;
#else
                return 0;
#endif
            }

            public static int UPxr_GetCurrentVolumeNumber()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            int currentvolm = 0;
            try
            {
                currentvolm = sysActivity.CallStatic<int>("pxr_GetAudionumber");
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_GetCurrentVolumeNumber Error :" + e.ToString());
            }
            return currentvolm;
#else
                return 0;
#endif
            }

            public static bool UPxr_VolumeUp()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_UpAudio");
                return true;
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_VolumeUp Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_VolumeDown()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_DownAudio");
                return true;
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_VolumeDown Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_SetVolumeNum(int volume)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_ChangeAudio", volume);
                return true;
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_SetVolumeNum Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static string UPxr_GetDeviceSN()
            {
                string serialNum = "UNKONWN";
#if UNITY_ANDROID && !UNITY_EDITOR
                serialNum = sysActivity.CallStatic<string>("getDeviceSN");
#endif
                return serialNum;
            }

            public static void UPxr_Sleep()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                sysActivity.CallStatic("pxr_Sleep");
#endif
            }

            public static Action<bool> BoolCallback;
            public static Action<int> IntCallback;
            public static Action<long> LongCallback;
            public static Action<string> StringCallback;

#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaObject tobHelper;
            private static AndroidJavaClass tobHelperClass;
#endif

            public static void UPxr_InitToBService()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelperClass = new AndroidJavaClass("com.pvr.tobservice.ToBServiceHelper");
                tobHelper = tobHelperClass.CallStatic<AndroidJavaObject>("getInstance");
                unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
            }

            public static void UPxr_SetUnityObjectName(string obj)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("setUnityObjectName", obj);
#endif
            }

            public static void UPxr_BindSystemService()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("bindTobService", currentActivity);
#endif
            }

            public static void UPxr_UnBindSystemService()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("unBindTobService", currentActivity);
#endif
            }

            private static AndroidJavaObject GetEnumType(Enum enumType)
            {
                AndroidJavaClass enumjs = new AndroidJavaClass("com.pvr.tobservice.enums" + enumType.GetType().ToString().Replace("Unity.XR.PXR.", ".PBS_"));
                AndroidJavaObject enumjo = enumjs.GetStatic<AndroidJavaObject>(enumType.ToString());
                return enumjo;
            }

            public static string UPxr_StateGetDeviceInfo(SystemInfoEnum type)
            {
                string result = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                result = tobHelper.Call<string>("pbsStateGetDeviceInfo", GetEnumType(type), 0);
#endif
                return result;
            }

            public static void UPxr_ControlSetDeviceAction(DeviceControlEnum deviceControl, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsControlSetDeviceAction", GetEnumType(deviceControl), null);
#endif
            }

            public static void UPxr_ControlAPPManager(PackageControlEnum packageControl, string path, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsControlAPPManger", GetEnumType(packageControl), path, 0, null);
#endif
            }

            public static void UPxr_ControlSetAutoConnectWIFI(string ssid, string pwd, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsControlSetAutoConnectWIFI", ssid, pwd, 0, null);
#endif
            }

            public static void UPxr_ControlClearAutoConnectWIFI(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsControlClearAutoConnectWIFI", null);
#endif
            }

            public static void UPxr_PropertySetHomeKey(HomeEventEnum eventEnum, HomeFunctionEnum function, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsPropertySetHomeKey", GetEnumType(eventEnum), GetEnumType(function), null);
#endif
            }

            public static void UPxr_PropertySetHomeKeyAll(HomeEventEnum eventEnum, HomeFunctionEnum function, int timesetup, string pkg, string className, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsPropertySetHomeKeyAll", GetEnumType(eventEnum), GetEnumType(function), timesetup, pkg, className, null);
#endif
            }

            public static void UPxr_PropertyDisablePowerKey(bool isSingleTap, bool enable, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsPropertyDisablePowerKey", isSingleTap, enable, null);
#endif
            }

            public static void UPxr_PropertySetScreenOffDelay(ScreenOffDelayTimeEnum timeEnum, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsPropertySetScreenOffDelay", GetEnumType(timeEnum), null);
#endif
            }

            public static void UPxr_PropertySetSleepDelay(SleepDelayTimeEnum timeEnum)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsPropertySetSleepDelay", GetEnumType(timeEnum));
#endif
            }

            public static void UPxr_SwitchSystemFunction(SystemFunctionSwitchEnum systemFunction, SwitchEnum switchEnum)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSwitchSystemFunction", GetEnumType(systemFunction), GetEnumType(switchEnum), 0);
#endif
            }

            public static void UPxr_SwitchSetUsbConfigurationOption(USBConfigModeEnum uSBConfigModeEnum)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSwitchSetUsbConfigurationOption", GetEnumType(uSBConfigModeEnum), 0);
#endif
            }

            public static void UPxr_ScreenOn()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsScreenOn");
#endif
            }

            public static void UPxr_ScreenOff()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsScreenOff");
#endif
            }

            public static void UPxr_AcquireWakeLock()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsAcquireWakeLock");
#endif
            }

            public static void UPxr_ReleaseWakeLock()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsReleaseWakeLock");
#endif
            }

            public static void UPxr_EnableEnterKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsEnableEnterKey");
#endif
            }

            public static void UPxr_DisableEnterKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsDisableEnterKey");
#endif
            }

            public static void UPxr_EnableVolumeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsEnableVolumeKey");
#endif
            }

            public static void UPxr_DisableVolumeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsDisableVolumeKey");
#endif
            }

            public static void UPxr_EnableBackKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsEnableBackKey");
#endif
            }

            public static void UPxr_DisableBackKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsDisableBackKey");
#endif
            }

            public static void UPxr_WriteConfigFileToDataLocal(string path, string content, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (callback != null) BoolCallback = callback;
            tobHelper.Call("pbsWriteConfigFileToDataLocal", path, content, null);
#endif
            }

            public static void UPxr_ResetAllKeyToDefault(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (callback != null) BoolCallback = callback;
            tobHelper.Call("pbsResetAllKeyToDefault", null);
#endif
            }

            public static void UPxr_SetAPPAsHome(SwitchEnum switchEnum, string packageName)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsAppSetAPPAsHomeTwo", GetEnumType(switchEnum), packageName);
#endif
            }

            public static void UPxr_KillAppsByPidOrPackageName(int[] pids, string[] packageNames)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsKillAppsByPidOrPackageName", pids, packageNames, 0);
#endif
            }

            public static void UPxr_KillBackgroundAppsWithWhiteList(string[] packageNames)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsKillBackgroundAppsWithWhiteList",packageNames, 0);
#endif
            }

            public static void UPxr_FreezeScreen(bool freeze)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsFreezeScreen", freeze);
#endif
            }

            public static void UPxr_OpenMiracast()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsOpenMiracast");
#endif
            }

            public static bool UPxr_IsMiracastOn()
            {
                bool value = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<bool>("pbsIsMiracastOn");
#endif
                return value;
            }

            public static void UPxr_CloseMiracast()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsCloseMiracast");
#endif
            }

            public static void UPxr_StartScan()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsStartScan");
#endif
            }

            public static void UPxr_StopScan()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsStopScan");
#endif
            }

            public static void UPxr_ConnectWifiDisplay(string modelJson)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsUnityConnectWifiDisplay", modelJson);
#endif
            }

            public static void UPxr_DisConnectWifiDisplay()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsDisConnectWifiDisplay");
#endif
            }

            public static void UPxr_ForgetWifiDisplay(string address)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsForgetWifiDisplay", address);
#endif
            }

            public static void UPxr_RenameWifiDisplay(string address, string newName)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsRenameWifiDisplay", address, newName);
#endif
            }

            public static void UPxr_SetWDModelsCallback()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSetWDModelsCallback", null);
#endif
            }

            public static void UPxr_SetWDJsonCallback()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSetWDJsonCallback", null);
#endif
            }

            public static void UPxr_UpdateWifiDisplays(Action<string> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) StringCallback = callback;
                tobHelper.Call("pbsUpdateWifiDisplays");
#endif
            }

            public static string UPxr_GetConnectedWD()
            {
                string result = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                result = tobHelper.Call<string>("pbsUnityGetConnectedWD");
#endif
                return result;
            }

            public static void UPxr_SwitchLargeSpaceScene(bool open, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsSwitchLargeSpaceScene",null, open, 0);
#endif
            }

            public static void UPxr_GetSwitchLargeSpaceStatus(Action<string> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) StringCallback = callback;
                tobHelper.Call("pbsGetSwitchLargeSpaceStatus",null, 0);
#endif
            }

            public static bool UPxr_SaveLargeSpaceMaps()
            {
                bool value = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<bool>("pbsSaveLargeSpaceMaps", 0);
#endif
                return value;
            }

            public static void UPxr_ExportMaps(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsExportMaps", null,0);
#endif
            }
                        
            public static void UPxr_ImportMaps(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsImportMaps", null, 0);
#endif
            }

            public static void UPxr_SetSecure(bool isOpen)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                sysActivity.CallStatic("SetSecure",currentActivity,isOpen);
#endif
            }

            public static int UPxr_GetColorRes(string name)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getColorRes", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetColorResError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetConfigInt(string name)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getConfigInt", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetConfigIntError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetConfigString(string name)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getConfigString", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetConfigStringError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetDrawableLocation(string name)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getDrawableLocation", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetDrawableLocationError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetTextSize(string name)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getTextSize", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetTextSizeError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetLangString(string name)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getLangString", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetLangStringError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetStringValue(string id, int type)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getStringValue", currentActivity, id, type);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetStringValueError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetIntValue(string id, int type)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getIntValue", currentActivity, id, type);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetIntValueError :" + e.ToString());
                }
#endif
                return value;
            }

            public static float UPxr_GetFloatValue(string id)
            {
                float value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<float>("getFloatValue", currentActivity, id);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetFloatValueError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetObjectOrArray(string id, int type)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getObjectOrArray", currentActivity, id, type);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetObjectOrArrayError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetCharSpace(string id)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getCharSpace", currentActivity, id);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetCharSpaceError :" + e.ToString());
                }
#endif
                return value;
            }

            public static float UPxr_RefreshRateChanged()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_RefreshRateChanged();
#else
                return -1.0f;
#endif
            }

            public static float[] UPxr_GetDisplayFrequenciesAvailable()
            {
                float[] configArray = null;
#if UNITY_ANDROID && !UNITY_EDITOR
                int configCount = 0;
                IntPtr configHandle = IntPtr.Zero;
                Pxr_GetDisplayRefreshRatesAvailable(ref configCount, ref configHandle);
                configArray = new float[configCount];
                Marshal.Copy(configHandle, configArray, 0, configCount);
#endif
                return configArray;
            }

            public static int UPxr_GetSensorStatus()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetSensorStatus();
#else
                return 0;
#endif
            }

            public static int UPxr_GetPredictedMainSensorStateNew(ref PxrSensorState2 sensorState, ref int sensorFrameIndex)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                int SDKVersion =  UPxr_GetConfigInt(ConfigType.PxrAPIVersion);
                if(SDKVersion >= 0x2000201){
                    double predictTime = UPxr_GetPredictedDisplayTime();
                    return Pxr_GetPredictedMainSensorState2(predictTime, ref sensorState, ref sensorFrameIndex);
                }else
                {
                    return 0;
                }
#else
                return 0;
#endif
            }

            public static void UPxr_SetLargeSpaceStatus(bool value) {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetLargeSpaceStatus(value);
#endif

            }

            public static void UPxr_SetLargeSpaceEnable(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetLargeSpaceEnable(value);
#endif

            }

        }

        public static class Boundary
        {
            private const string TAG = "[PXR_Plugin/Boundary]";

            public static PxrBoundaryTriggerInfo UPxr_TestNodeIsInBoundary(BoundaryTrackingNode node, BoundaryType boundaryType)
            {
                PxrBoundaryTriggerInfo testResult = new PxrBoundaryTriggerInfo();
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_TestNodeIsInBoundary(node, boundaryType == BoundaryType.PlayArea, ref testResult);
                testResult.closestPoint.z = -testResult.closestPoint.z;
                testResult.closestPointNormal.z = -testResult.closestPointNormal.z;
                if (!testResult.valid)
                {
                    PLog.d(TAG, string.Format("Pxr_TestBoundaryNode({0}, {1}) API call failed!", node, boundaryType));
                }
#endif
                return testResult;
            }

            public static PxrBoundaryTriggerInfo UPxr_TestPointIsInBoundary(PxrVector3f point, BoundaryType boundaryType)
            {
                PxrBoundaryTriggerInfo testResult = new PxrBoundaryTriggerInfo();
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_TestPointIsInBoundary(ref point, boundaryType == BoundaryType.PlayArea, ref testResult);

                if (!testResult.valid)
                {
                    PLog.d(TAG, string.Format("Pxr_TestBoundaryPoint({0}, {1}) API call failed!", point, boundaryType));
                }

#endif
                return testResult;
            }

            public static Vector3[] UPxr_GetBoundaryGeometry(BoundaryType boundaryType)
            {
                Vector3[] points = new Vector3[1];
#if UNITY_ANDROID && !UNITY_EDITOR

                UInt32 pointsCountOutput = 0;
                PxrVector3f[] outPointsFirst = null;
                Pxr_GetBoundaryGeometry(boundaryType == BoundaryType.PlayArea, 0, ref pointsCountOutput, outPointsFirst);
                if (pointsCountOutput <= 0)
                {
                    PLog.d(TAG, "Boundary geometry point count = " + pointsCountOutput);
                    return null;
                }

                PxrVector3f[] outPoints = new PxrVector3f[pointsCountOutput];
                Pxr_GetBoundaryGeometry(boundaryType == BoundaryType.PlayArea, pointsCountOutput, ref pointsCountOutput, outPoints);

                points = new Vector3[pointsCountOutput];
                for (int i = 0; i < pointsCountOutput; i++)
                {
                    points[i] = new Vector3()
                    {
                        x = outPoints[i].x,
                        y = outPoints[i].y,
                        z = -outPoints[i].z,
                    };
                }
#endif
                return points;
            }

            public static Vector3 UPxr_GetBoundaryDimensions(BoundaryType boundaryType)
            {
                // float x = 0, y = 0, z = 0;
                PxrVector3f dimension = new PxrVector3f();
#if UNITY_ANDROID && !UNITY_EDITOR
                int ret = 0;
                Pxr_GetBoundaryDimensions( boundaryType == BoundaryType.PlayArea, out dimension);
#endif
                return new Vector3(dimension.x, dimension.y, dimension.z);
            }

            public static void UPxr_SetBoundaryVisiable(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetBoundaryVisible(value);
#endif
            }

            public static bool UPxr_GetBoundaryVisiable()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetBoundaryVisible();
#else
                return true;
#endif
            }

            public static bool UPxr_GetBoundaryConfigured()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetBoundaryConfigured();
#else
                return true;
#endif
            }

            public static bool UPxr_GetBoundaryEnabled()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetBoundaryEnabled();
#else
                return true;
#endif
            }

            public static int UPxr_SetSeeThroughBackground(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetSeeThroughBackground(value);
#else
                return 0;
#endif
            }

            public static int UPxr_GetDialogState()
            {
                var state = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = Pxr_GetDialogState();
                }
                catch (Exception e)
                {
                    Debug.Log("PXRLog UPxr_GetDialogStateError :" + e.ToString());
                }
#endif
                return state;
            }

            public static int UPxr_GetSeeThroughState()
            {
                var state = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = Pxr_GetSeeThroughState();
                }
                catch (Exception e)
                {
                    Debug.Log("PXRLog UPxr_GetSeeThroughState :" + e.ToString());
                }
#endif
                return state;
            }
        }

        public static class Render
        {
            private const string TAG = "[PXR_Plugin/Render]";

            public static void UPxr_SetFoveationLevel(FoveationLevel level)
            {
                PLog.i(TAG, "UPxr_SetFoveationLevel()");
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetFoveationLevelEnable((int)level);
#endif
            }

            public static FoveationLevel UPxr_GetFoveationLevel()
            {
                PLog.i(TAG, "UPxr_GetFoveationLevel()");
#if UNITY_ANDROID && !UNITY_EDITOR
            return Pxr_GetFoveationLevel();
#else
                return FoveationLevel.None;
#endif
            }

            public static int UPxr_SetFoveationParameters(float foveationGainX, float foveationGainY, float foveationArea, float foveationMinimum)
            {
                PLog.i(TAG, "UPxr_SetFoveationParameters()");
#if UNITY_ANDROID && !UNITY_EDITOR

                FoveationParams foveationParams = new FoveationParams();
                foveationParams.foveationGainX = foveationGainX;
                foveationParams.foveationGainY = foveationGainY;
                foveationParams.foveationArea = foveationArea;
                foveationParams.foveationMinimum = foveationMinimum;

                return Pxr_SetFoveationParams(foveationParams);
#else
                return 0;
#endif
            }

            public static int UPxr_GetFrustum(EyeType eye, ref float fovLeft, ref float fovRight, ref float fovUp, ref float fovDown, ref float near, ref float far)
            {
                PLog.i(TAG, "UPxr_GetFrustum()");
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetFrustum(eye, ref fovLeft, ref fovRight, ref fovUp, ref fovDown, ref near, ref far);
#else
                return 0;
#endif
            }

            public static int UPxr_SetFrustum(EyeType eye, float fovLeft, float fovRight, float fovUp, float fovDown, float near, float far)
            {
                int value = 1;
                PLog.i(TAG, "UPxr_SetFrustum()");
#if UNITY_ANDROID && !UNITY_EDITOR
                value = Pxr_SetFrustum(eye, fovLeft, fovRight, fovUp, fovDown, near, far);
#endif
                return value;
            }
            public static void UPxr_CreateLayer(IntPtr layerParam)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_CreateLayer(layerParam);
#endif
            }

            public static void UPxr_CreateLayerParam(PxrLayerParam layerParam)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_CreateLayerParam(layerParam);
#endif
            }

            public static int UPxr_GetLayerNextImageIndex(int layerId, ref int imageIndex)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetLayerNextImageIndex(layerId, ref imageIndex);
#else
                return 0;
#endif
            }

            public static int UPxr_GetLayerImageCount(int layerId, EyeType eye, ref UInt32 imageCount)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetLayerImageCount(layerId, eye, ref imageCount);
#else
                return 0;
#endif
            }

            public static int UPxr_GetLayerImage(int layerId, EyeType eye, int imageIndex, ref UInt64 image)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetLayerImage(layerId, eye, imageIndex, ref image);
#else
                return 0;
#endif
            }

            public static void UPxr_GetLayerImagePtr(int layerId, EyeType eye, int imageIndex, ref IntPtr image)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetLayerImagePtr(layerId, eye, imageIndex, ref image);
#endif
            }

            public static int UPxr_GetLayerAndroidSurface(int layerId, EyeType eye, ref IntPtr androidSurface)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetLayerAndroidSurface(layerId, eye, ref androidSurface);
#else
                return 0;
#endif
            }

            public static int UPxr_DestroyLayer(int layerId)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_DestroyLayer(layerId);
#else
                return 0;
#endif
            }

            public static void UPxr_DestroyLayerByRender(int layerId)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_DestroyLayerByRender(layerId);
#endif
            }

            public static int UPxr_SubmitLayer(IntPtr layer)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SubmitLayer(layer);
#else
                return 0;
#endif
            }

            public static int UPxr_SubmitLayerQuad(PxrLayerQuad layer)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SubmitLayerQuad(layer);
#else
                return 0;
#endif
            }

            public static int UPxr_SubmitLayerCylinder(PxrLayerCylinder layer)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SubmitLayerCylinder(layer);
#else
                return 0;
#endif
            }

            public static int UPxr_SubmitLayerEquirect(PxrLayerEquirect layer)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SubmitLayerEquirect(layer);
#else
                return 0;
#endif
            }

        }

        public static class Sensor
        {
            private const string TAG = "[PXR_Plugin/Sensor]";

#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            private static AndroidJavaClass sysActivity = new AndroidJavaClass("com.psmart.aosoperation.SysActivity");
#endif

            public static int UPxr_ResetSensor(ResetSensorOption resetSensorOption)
            {
                PLog.i(TAG, string.Format("UPxr_ResetSensor : {0}", resetSensorOption));
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_ResetSensor(resetSensorOption);
#else
                return 0;
#endif
            }

            public static int UPvr_Enable6DofModule(bool enable)
            {
                PLog.i(TAG, string.Format("UPvr_Enable6DofModule : {0}", enable));
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetConfigInt(ConfigType.Ability6Dof, enable?1:0);
#else
                return 0;
#endif
            }

            public static void UPxr_InitPsensor()
            {
                PLog.i(TAG, "UPxr_InitPsensor()");
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    sysActivity.CallStatic("initPsensor", currentActivity);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "Error :" + e.ToString());
                }
#endif
            }

            public static int UPxr_GetPSensorState()
            {
                PLog.i(TAG, "UPxr_GetPSensorState()");
                int psensor = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    psensor = sysActivity.CallStatic<int>("getPsensorState");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "Error :" + e.ToString());
                }
#endif
                return psensor;
            }

            public static void UPxr_UnregisterPsensor()
            {
                PLog.i(TAG, "UPxr_UnregisterPsensor()");
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    sysActivity.CallStatic("unregisterListener");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "Error :" + e.ToString());
                }
#endif
            }

            public static int UPxr_SetSensorLostCustomMode(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetSensorLostCustomMode(value);
#else
                return 0;
#endif
            }

            public static int UPxr_SetSensorLostCMST(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetSensorLostCMST(value);
#else
                return 0;
#endif
            }

        }

        public static class PlatformSetting
        {
            private const string TAG = "[PXR_Plugin/PlatformSetting]";

#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaClass verifyTool = new AndroidJavaClass("com.psmart.aosoperation.VerifyTool");
            private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
            public static PXR_PlatformSetting.simulationType UPxr_IsCurrentDeviceValid()
            {
                if (PXR_PlatformSetting.Instance.entitlementCheckSimulation)
                {
                    if (PXR_PlatformSetting.Instance.deviceSN.Count <= 0)
                    {
                        return PXR_PlatformSetting.simulationType.Null;
                    }
                    else
                    {
                        foreach (var t in PXR_PlatformSetting.Instance.deviceSN)
                        {
                            if (System.UPxr_GetDeviceSN() == t)
                            {
                                return PXR_PlatformSetting.simulationType.Valid;
                            }
                        }

                        return PXR_PlatformSetting.simulationType.Invalid;
                    }
                }
                else
                {
                    return PXR_PlatformSetting.simulationType.Invalid;
                }
            }

            public static void UPxr_BindVerifyService(string objectName)
            {
                AndroidJavaObject VerifyTool = new AndroidJavaObject("com.psmart.aosoperation.VerifyTool");
                PLog.i(TAG, "UPxr_BindVerifyService()");
#if UNITY_ANDROID && !UNITY_EDITOR
                VerifyTool.Call<bool>("bindVerifyService", currentActivity, objectName);
#endif
            }

            public static bool UPxr_AppEntitlementCheck(string appid)
            {
                PLog.i(TAG, "UPxr_AppEntitlementCheck()");
                bool state = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<bool>("verifyAPP", currentActivity, appid, "");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_AppEntitlementCheck Error :" + e.ToString());
                }
#endif
                return state;
            }

            public static bool UPxr_KeyEntitlementCheck(string publicKey)
            {
                PLog.i(TAG, "UPxr_KeyEntitlementCheck()");
                bool state = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<bool>("verifyAPP", currentActivity, "", publicKey);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_KeyEntitlementCheck Error :" + e.ToString());
                }
#endif
                return state;
            }

            //0:success -1:invalid params -2:service not exist -3:time out
            public static int UPxr_AppEntitlementCheckExtra(string appId)
            {
                PLog.i(TAG, "UPxr_AppEntitlementCheckExtra()");
                int state = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<int>("verifyAPPExt", currentActivity, appId, "");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_AppEntitlementCheckExtra Error :" + e.ToString());
                }
#endif
                return state;
            }

            //0:success -1:invalid params -2:service not exist -3:time out
            public static int UPxr_KeyEntitlementCheckExtra(string publicKey)
            {
                PLog.i(TAG, "UPxr_KeyEntitlementCheckExtra()");
                int state = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<int>("verifyAPPExt", currentActivity, "", publicKey);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_KeyEntitlementCheckExtra Error :" + e.ToString());
                }
#endif
                return state;
            }
        }

        public static class Controller
        {
            private const string TAG = "[PXR_Plugin/Controller]";

            public static int UPxr_SetControllerVibration(UInt32 hand, float strength, int time)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetControllerVibration(hand,strength, time);
#else
                return 0;
#endif
            }

            public static int UPxr_GetControllerType()
            {
                var type = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                PxrControllerCapability capability = new PxrControllerCapability();
                Pxr_GetControllerCapabilities(0,ref capability);
                type = (int)capability.type;
#endif
                PLog.i(TAG, "UPxr_GetControllerType()" + type);
                return type;
            }

            public static int UPxr_SetControllerMainInputHandle(UInt32 hand)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetControllerMainInputHandle(hand);
#else
                return 0;
#endif
            }

            public static PXR_Input.Controller UPxr_GetControllerMainInputHandle()
            {
                var hand = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetControllerMainInputHandle(ref hand);
#endif
                PLog.i(TAG, "Pxr_GetControllerMainInputHandle()" + hand.ToString());
                return (PXR_Input.Controller)hand;
            }

            public static int UPxr_GetControllerTrackingState(UInt32 deviceID, double predictTime, float[] headSensorData, ref PxrControllerTracking tracking)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetControllerTrackingState(deviceID,predictTime,headSensorData, ref tracking);
#else
                return 0;
#endif
            }

            public static void UPxr_SetControllerOriginOffset(int controllerID, Vector3 offset)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetControllerOriginOffset(controllerID, offset);
#endif
            }

        }

        public static class PassThrough
        {
            public static int UPxr_PassThroughStart()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraStart();
#else
                return 0;
#endif
            }

            public static int UPxr_PassThroughStop()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraStop();
#else
                return 0;
#endif
            }

            public static int UPxr_PassThroughDestroy()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraDestroy();
#else
                return 0;
#endif
            }

            public static IntPtr UPxr_PassThroughGetRenderEventFunc()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraGetRenderEventFunc();
#else
                return IntPtr.Zero;
#endif
            }

            public static void UPxr_PassThroughSetRenderEventPending()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_CameraSetRenderEventPending();
#endif
            }

            public static void UPxr_PassThroughWaitForRenderEvent()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_CameraWaitForRenderEvent();
#endif
            }

            public static int UPxr_PassThroughUpdateFrame(int eye)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraUpdateFrame(eye);
#else
                return 0;
#endif
            }

            public static int UPxr_PassThroughCreateTexturesMainThread()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraCreateTexturesMainThread();
#else
                return 0;
#endif
            }

            public static int UPxr_PassThroughDeleteTexturesMainThread()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraDeleteTexturesMainThread();
#else
                return 0;
#endif
            }

            public static int UPxr_PassThroughUpdateTexturesMainThread()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_CameraUpdateTexturesMainThread();
#else
                return 0;
#endif
            }
        }
    }
}