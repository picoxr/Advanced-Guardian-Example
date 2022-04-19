// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public class PXR_OverLay : MonoBehaviour, IComparable<PXR_OverLay>
    {
        private const string TAG = "[PXR_CompositeLayers]";
        public static List<PXR_OverLay> Instances = new List<PXR_OverLay>();

        private static int layerID = 0;
        public int layerIndex;
        public int layerDepth;
        public int imageIndex = 0;
        public OverlayType overlayType = OverlayType.Overlay;
        public OverlayShape overlayShape = OverlayShape.Quad;
        public Transform layerTransform;
        public Camera xrRig;

        public Texture[] layerTextures = new Texture[2] { null, null };
        public bool isDynamic = false;
        public int[] layerTextureIds = new int[2];
        public Matrix4x4[] mvMatrixs = new Matrix4x4[2];
        public Vector3[] modelScales = new Vector3[2];
        public Quaternion[] modelRotations = new Quaternion[2];
        public Vector3[] modelTranslations = new Vector3[2];
        public Quaternion[] cameraRotations = new Quaternion[2];
        public Vector3[] cameraTranslations = new Vector3[2];
        public Camera[] layerEyeCamera = new Camera[2];

        public bool overrideColorScaleAndOffset = false;
        public Vector4 colorScale = Vector4.one;
        public Vector4 colorOffset = Vector4.zero;

        private Vector4 overlayLayerColorScaleDefault = Vector4.one;
        private Vector4 overlayLayerColorOffsetDefault = Vector4.zero;

        public bool isExternalAndroidSurface = false;
        public IntPtr externalAndroidSurfaceObject = IntPtr.Zero;
        public delegate void ExternalAndroidSurfaceObjectCreated();
        public ExternalAndroidSurfaceObjectCreated externalAndroidSurfaceObjectCreated = null;

        private bool toCreateSwapChain = false;
        private bool toCopyRT = false;
        private bool copiedRT = false;
        private int eyeCount = 2;
        private UInt32 imageCounts = 0;
        private PxrLayerParam layerParam = new PxrLayerParam();
        private struct LayerTexture
        {
            public Texture[] swapChain;
        };
        private LayerTexture[] layerTexturesInfo;

        public int CompareTo(PXR_OverLay other)
        {
            return layerDepth.CompareTo(other.layerDepth);
        }

        private void Awake()
        {
            xrRig = Camera.main;
            Instances.Add(this);
            //layerIndex = Instances.IndexOf(this) + 1; // 0 is for Eye Buffer

            layerEyeCamera[0] = xrRig;
            layerEyeCamera[1] = xrRig;

            layerTransform = GetComponent<Transform>();
#if UNITY_ANDROID && !UNITY_EDITOR
            if (layerTransform != null)
            {
                MeshRenderer render = layerTransform.GetComponent<MeshRenderer>();
                if (render != null)
                {
                    render.enabled = false;
                }
            }
#endif

            InitializeBuffer();
        }

        private void OnDestroy()
        {
            DestroyLayerTextures();
            Instances.Remove(this);
            PXR_Plugin.Render.UPxr_DestroyLayerByRender(layerIndex);
        }

        public void RefreshCamera(Camera cam)
        {
            xrRig = cam;
            layerEyeCamera[0] = xrRig;
            layerEyeCamera[1] = xrRig;
        }

        private void InitializeBuffer()
        {
            layerID++;
            layerIndex = layerID;
            if (overlayShape == 0)
            {
                overlayShape = OverlayShape.Quad;
            }

            layerParam.layerId = layerIndex;
            layerParam.layerShape = overlayShape;
            layerParam.layerType = overlayType;
            layerParam.format = (UInt64)RenderTextureFormat.Default;

            if (layerTextures[0] == null && layerTextures[1] != null)
            {
                layerTextures[0] = layerTextures[1];
            }

            if (layerTextures[1] != null)
            {
                layerParam.width = (uint)layerTextures[1].width;
                layerParam.height = (uint)layerTextures[1].height;
            }
            else
            {
                layerParam.width = (uint)PXR_Plugin.System.UPxr_GetConfigInt(ConfigType.RenderTextureWidth);
                layerParam.height = (uint)PXR_Plugin.System.UPxr_GetConfigInt(ConfigType.RenderTextureHeight);
            }

            layerParam.sampleCount = 1;
            layerParam.faceCount = 1;
            layerParam.arraySize = 1;
            layerParam.mipmapCount = 1;

            if (isExternalAndroidSurface)
            {
                layerParam.width = 1024;
                layerParam.height = 1024;
                layerParam.layerFlags = (UInt32)PxrLayerCreateFlags.PxrLayerFlagAndroidSurface;
                layerParam.layerLayout = LayerLayout.Mono;
                IntPtr layerParamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(layerParam));
                Marshal.StructureToPtr(layerParam, layerParamPtr, false);
                PXR_Plugin.Render.UPxr_CreateLayer(layerParamPtr);
                Marshal.FreeHGlobal(layerParamPtr);
            }
            else
            {
                if (isDynamic)
                {
                    layerParam.layerFlags = 0;
                }
                else
                {
                    layerParam.layerFlags = (UInt32)PxrLayerCreateFlags.PxrLayerFlagStaticImage;
                }

                if ((layerTextures[0] != null && layerTextures[1] != null && layerTextures[0] == layerTextures[1]) || layerTextures[1] == null)
                {
                    eyeCount = 1;
                    layerParam.layerLayout = LayerLayout.Mono;
                }
                else
                {
                    eyeCount = 2;
                    layerParam.layerLayout = LayerLayout.Stereo;
                }

                PXR_Plugin.Render.UPxr_CreateLayerParam(layerParam);
                toCreateSwapChain = true;
                CreateLayerTexture();
            }
        }

        public void CreateExternalSurface(PXR_OverLay overlayInstance)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (overlayInstance.externalAndroidSurfaceObject == IntPtr.Zero)
        {
            PXR_Plugin.Render.UPxr_GetLayerAndroidSurface(overlayInstance.layerIndex, 0, ref overlayInstance.externalAndroidSurfaceObject);
            PLog.i(TAG, string.Format("CreateExternalSurface: Overlay Type:{0}, LayerDepth:{1}, SurfaceObject:{2}", overlayInstance.overlayType, overlayInstance.layerIndex, overlayInstance.externalAndroidSurfaceObject));

            if (overlayInstance.externalAndroidSurfaceObject != IntPtr.Zero)
            {
                if (overlayInstance.externalAndroidSurfaceObjectCreated != null)
                {
                    overlayInstance.externalAndroidSurfaceObjectCreated();
                }
            }
        }
#endif
        }

        public void UpdateCoords()
        {
            if (layerTransform == null || !layerTransform.gameObject.activeSelf)
            {
                return;
            }

            if (layerEyeCamera[0] == null || layerEyeCamera[1] == null)
            {
                return;
            }

            for (int i = 0; i < mvMatrixs.Length; i++)
            {
                mvMatrixs[i] = layerEyeCamera[i].worldToCameraMatrix * layerTransform.localToWorldMatrix;

                modelScales[i] = layerTransform.localScale;
                modelRotations[i] = layerTransform.rotation;
                modelTranslations[i] = layerTransform.position;
                cameraRotations[i] = layerEyeCamera[i].transform.rotation;
                cameraTranslations[i] = layerEyeCamera[i].transform.position;
            }
        }

        public bool CreateLayerTexture()
        {
            if (!toCreateSwapChain)
            {
                return false;
            }

            if (layerTexturesInfo == null)
                layerTexturesInfo = new LayerTexture[eyeCount];
            for (int i = 0; i < eyeCount; i++)
            {
                int ret = PXR_Plugin.Render.UPxr_GetLayerImageCount(layerIndex, (EyeType)i, ref imageCounts);
                if (ret != 0 || imageCounts < 1)
                {
                    return false;
                }

                if (layerTexturesInfo[i].swapChain == null)
                {
                    layerTexturesInfo[i].swapChain = new Texture[imageCounts];
                }

                for (int j = 0; j < imageCounts; j++)
                {
                    IntPtr ptr = IntPtr.Zero;
                    PXR_Plugin.Render.UPxr_GetLayerImagePtr(layerIndex, (EyeType)i, j, ref ptr);

                    bool useMipmaps = (layerParam.mipmapCount > 1);
                    if (ptr == IntPtr.Zero)
                    {
                        return false;
                    }

                    Texture sc = Texture2D.CreateExternalTexture((int)layerParam.width, (int)layerParam.height, TextureFormat.RGBA32, useMipmaps, true, ptr);

                    if (sc == null)
                    {
                        return false;
                    }

                    layerTexturesInfo[i].swapChain[j] = sc;
                }
            }

            toCreateSwapChain = false;
            toCopyRT = true;
            copiedRT = false;

            return true;
        }

        public bool CopyRT()
        {
            if (!toCopyRT)
            {
                return copiedRT;
            }

            if (!isDynamic && copiedRT)
            {
                return copiedRT;
            }

            RenderTextureFormat rtFormat = RenderTextureFormat.ARGB32;

            for (int eyeId = 0; eyeId < eyeCount; ++eyeId)
            {
                Texture dstT = layerTexturesInfo[eyeId].swapChain[imageIndex];

                if (dstT == null)
                    continue;

                for (int mip = 0; mip < (int)layerParam.mipmapCount; ++mip)
                {
                    bool isLinear = (QualitySettings.activeColorSpace == ColorSpace.Linear);
                    var rt = layerTextures[eyeId] as RenderTexture;
                    bool bypassBlit = !isLinear && rt != null && rt.format == rtFormat;
                    RenderTexture tempDstRT = null;

                    if (!bypassBlit)
                    {
                        int width = (int)layerParam.width >> mip;
                        if (width < 1) width = 1;
                        int height = (int)layerParam.height >> mip;
                        if (height < 1) height = 1;

                        RenderTextureDescriptor descriptor = new RenderTextureDescriptor(width, height, rtFormat, 0);
                        descriptor.msaaSamples = (int)layerParam.sampleCount;
                        descriptor.useMipMap = true;
                        descriptor.autoGenerateMips = false;
                        descriptor.sRGB = false;

                        tempDstRT = RenderTexture.GetTemporary(descriptor);

                        if (!tempDstRT.IsCreated())
                        {
                            tempDstRT.Create();
                        }
                        tempDstRT.DiscardContents();
                    }

                    if (bypassBlit)
                    {
                        Graphics.CopyTexture(layerTextures[eyeId], 0, mip, dstT, 0, mip);
                    }
                    else
                    {
                        Graphics.Blit(layerTextures[eyeId], tempDstRT);
                        Graphics.CopyTexture(tempDstRT, 0, 0, dstT, 0, mip);
                    }

                    if (tempDstRT != null)
                    {
                        RenderTexture.ReleaseTemporary(tempDstRT);
                    }
                }
            }
            copiedRT = true;

            return copiedRT;
        }

        public void SetTexture(Texture texture, bool dynamic)
        {
            if (isExternalAndroidSurface)
            {
                PLog.w(TAG, "Not support setTexture !");
                return;
            }

            toCopyRT = false;
            PXR_Plugin.Render.UPxr_DestroyLayerByRender(layerIndex);
            DestroyLayerTextures();
            for (int i = 0; i < layerTextures.Length; i++)
            {
                layerTextures[i] = texture;
            }

            isDynamic = dynamic;
            InitializeBuffer();
        }

        private void DestroyLayerTextures()
        {
            if (isExternalAndroidSurface)
            {
                return;
            }

            for (int eyeId = 0; layerTexturesInfo != null && eyeId < eyeCount; ++eyeId)
            {
                if (layerTexturesInfo[eyeId].swapChain != null)
                {
                    for (int stage = 0; stage < imageCounts; ++stage)
                        DestroyImmediate(layerTexturesInfo[eyeId].swapChain[stage]);
                }
            }

            layerTexturesInfo = null;
        }

        public void SetLayerColorScaleAndOffset(Vector4 scale, Vector4 offset)
        {
            colorScale = scale;
            colorOffset = offset;
        }

        public Vector4 GetLayerColorScale()
        {
            if (!overrideColorScaleAndOffset)
            {
                return overlayLayerColorScaleDefault;
            }
            return colorScale;
        }

        public Vector4 GetLayerColorOffset()
        {
            if (!overrideColorScaleAndOffset)
            {
                return overlayLayerColorOffsetDefault;
            }
            return colorOffset;
        }

        public enum OverlayShape
        {
            Quad = 1,
            Cylinder = 2,
            Equirect = 3
        }

        public enum OverlayType
        {
            Overlay = 0,
            Underlay = 1
        }

        public enum LayerLayout
        {
            Stereo = 0,
            DoubleWide = 1,
            Array = 2,
            Mono = 3
        }
    }
}