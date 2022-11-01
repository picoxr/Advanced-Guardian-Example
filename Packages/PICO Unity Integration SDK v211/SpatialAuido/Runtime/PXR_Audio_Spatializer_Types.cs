//  Copyright © 2015-2022 Pico Technology Co., Ltd. All Rights Reserved.

using System.Runtime.InteropServices;
using UnityEngine;

namespace PXR_Audio
{
    namespace Spatializer
    {
        public enum Result
        {
            Error = -1,
            Success = 0,
            SourceNotFound = -1001,
            SourceDataNotFound = -1002,
            SceneNotFound = -1003,
            SceneMeshNotFound = -1004,
            IllegalValue = -1005,
            ContextNotCreated = -1006,
            ContextNotReady = -1007,
            ContextRepeatedInitialization = -1008,
            EnvironmentalAcousticsDisabled = -1009,
        };

        public enum PlaybackMode
        {
            BinauralOut,
            LoudspeakersOut,
        };

        public enum LateReverbUpdatingMode
        {
            RealtimeLateReverb = 0,
            BakedLateReverb = 1,
            SharedSpectralLateReverb = 2,
        };

        public enum LateReverbRenderingMode
        {
            IrLateReverb = 0,
            SpectralLateReverb = 1,
        };

        public enum RenderingMode
        {
            LowQuality = 0, // 1st order ambisonic
            MediumQuality = 1, // 3rd order ambisonic
            HighQuality = 2, // 5th order ambisonic
            AmbisonicFirstOrder,
            AmbisonicSecondOrder,
            AmbisonicThirdOrder,
            AmbisonicFourthOrder,
            AmbisonicFifthOrder,
            AmbisonicSixthOrder,
            AmbisonicSeventhOrder,
        };

        public enum SourceMode
        {
            Spatialize = 0,
            Bypass = 1,
        };

        public enum IRUpdateMethod
        {
            PerPartitionSwapping = 0,
            InterPartitionLinearCrossFade = 1,
            InterPartitionPowerComplementaryCrossFade = 2
        };

        public enum AcousticsMaterial
        {
            AcousticTile,
            Brick,
            BrickPainted,
            Carpet,
            CarpetHeavy,
            CarpetHeavyPadded,
            CeramicTile,
            Concrete,
            ConcreteRough,
            ConcreteBlock,
            ConcreteBlockPainted,
            Curtain,
            Foliage,
            Glass,
            GlassHeavy,
            Grass,
            Gravel,
            GypsumBoard,
            PlasterOnBrick,
            PlasterOnConcreteBlock,
            Soil,
            SoundProof,
            Snow,
            Steel,
            Water,
            WoodThin,
            WoodThick,
            WoodFloor,
            WoodOnConcrete,
            Custom
        };

        public enum AmbisonicNormalizationType
        {
            SN3D,
            N3D
        };

        public enum SourceAttenuationMode
        {
            None = 0,           // 引擎不依据距离计算衰减
            Fixed = 1,          // 与None完全一致
            InverseSquare = 2,  // 引擎 InverseSquare Law 计算距离衰减
            Customized = 3,     // 依据外部传入的 Callback 计算距离衰减
        };

        public enum SpatializerApiImpl
        {
            unity,
            wwise,
        }

        public delegate float DistanceAttenuationCallback(float distance, float rangeMin, float rangeMax);

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeVector3f
        {
            public float x; //float[3]
            public float y;
            public float z;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SourceConfig
        {
            [MarshalAs(UnmanagedType.U4)] public SourceMode mode;
            public NativeVector3f position;
            public NativeVector3f front;
            public NativeVector3f up;
            public float radius;

            public float directivityAlpha; // Weighting balance between figure of eight pattern and circular pattern for

            // source emission in range [0, 1].
            // A value of 0 results in a circular pattern.
            // A value of 0.5 results in a cardioid pattern.
            // A value of 1 results in a figure of eight pattern.
            public float
                directivityOrder; // Order applied to computed directivity. Higher values will result in narrower and

            // sharper directivity patterns. Range [1, inf).
            [MarshalAs(UnmanagedType.U1)] public bool useDirectPathSpread;

            public float directPathSpread; // Alternatively, we could use spread param directly.

            // This is useful when audio middleware specifies spread value by itself.
            public float sourceGain; // Master gain of sound source.
            public float reflectionGain; // Reflection gain relative to default (master gain).
            
            [MarshalAs(UnmanagedType.U1)] public bool enableDoppler;

            public SourceConfig(SourceMode inMode)
            {
                mode = inMode;
                position.x = 0.0f;
                position.y = 0.0f;
                position.z = 0.0f;
                front.x = 0.0f;
                front.y = 0.0f;
                front.z = -1.0f;
                up.x = 0.0f;
                up.y = 1.0f;
                up.z = 0.0f;
                radius = 0.1f;
                directivityAlpha = 0.0f;
                directivityOrder = 1.0f;
                useDirectPathSpread = false;
                directPathSpread = 0.0f;
                sourceGain = 1.0f;
                reflectionGain = 1.0f;
                enableDoppler = false;
            }
        }
    }
}