// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using System;
using Unity.XR.PXR;
using UnityEngine;

[UnityEngine.Scripting.Preserve]
public class PXR_PassThroughSystem : Subsystem<PXR_PassThroughDescriptor>
{
    bool isRunning = false;
    public override bool running => isRunning;

    PXR_PassThroughProvider ptProvider = new PXR_PassThroughProvider();

#if !UNITY_2020_1_OR_NEWER
    bool isDestroyed;
#endif

    public override void Start()
    {
        if (!isRunning)
        {
            ptProvider.Start();
        }
        isRunning = true;
    }

    public override void Stop()
    {
        if (isRunning)
        {
            ptProvider.Stop();
        }
        isRunning = false;
    }

    public void UpdateTextures()
    {
        ptProvider.UpdateTextures();
    }

    protected override void OnDestroy()
    {
#if !UNITY_2020_1_OR_NEWER
        if (isDestroyed)
            return;
        isDestroyed = true;
#endif
        Stop();
        isRunning = false;
        ptProvider.Destroy();
    }

    public int UpdateCameraTextureID(int eye)
    {
        return PXR_Plugin.PassThrough.UPxr_PassThroughUpdateFrame(eye);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Register()
    {
        PXR_PassThroughDescriptor.Cinfo info = new PXR_PassThroughDescriptor.Cinfo();
        info.id = "PicoXR Camera";
        info.ImplementaionType = typeof(PXR_PassThroughSystem);
        PXR_PassThroughDescriptor descriptor = PXR_PassThroughDescriptor.Create(info);
        SubsystemRegistration.CreateDescriptor(descriptor);
    }


    class PXR_PassThroughProvider
    {
        IntPtr renderEventFunc;

        public PXR_PassThroughProvider()
        {
            if (SystemInfo.graphicsMultiThreaded)
            {
                renderEventFunc = PXR_Plugin.PassThrough.UPxr_PassThroughGetRenderEventFunc();
            }
        }

        public void Start()
        {
            CreateTexture();
            PXR_Plugin.PassThrough.UPxr_PassThroughStart();
        }

        public void Stop()
        {
            PXR_Plugin.PassThrough.UPxr_PassThroughStop();
        }

        public void Destroy()
        {
            PXR_Plugin.PassThrough.UPxr_PassThroughDestroy();
        }

        void IssueRenderEventAndWaitForCompletion(RenderEvent renderEvent)
        {
            if (renderEventFunc != IntPtr.Zero)
            {
                PXR_Plugin.PassThrough.UPxr_PassThroughSetRenderEventPending();
                GL.IssuePluginEvent(renderEventFunc, (int)renderEvent);
                PXR_Plugin.PassThrough.UPxr_PassThroughWaitForRenderEvent();
            }
        }

        void CreateTexture()
        {
            if (SystemInfo.graphicsMultiThreaded)
            {
                IssueRenderEventAndWaitForCompletion(RenderEvent.CreateTexture);
            }
            else
            {
                PXR_Plugin.PassThrough.UPxr_PassThroughCreateTexturesMainThread();
            }
        }

        void DeleteTexture()
        {
            if (SystemInfo.graphicsMultiThreaded)
            {
                IssueRenderEventAndWaitForCompletion(RenderEvent.DeleteTexture);
            }
            else
            {
                PXR_Plugin.PassThrough.UPxr_PassThroughDeleteTexturesMainThread();
            }
        }

        public void UpdateTextures()
        {
            if (SystemInfo.graphicsMultiThreaded)
            {
                IssueRenderEventAndWaitForCompletion(RenderEvent.UpdateTexture);
            }
            else
            {
                PXR_Plugin.PassThrough.UPxr_PassThroughUpdateTexturesMainThread();
            }
        }
    }
}
