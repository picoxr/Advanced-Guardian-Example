/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Unity.XR.PXR
{
    public class PXR_FPS : MonoBehaviour
    {
        private Text fpsText;

        private float updateInterval = 1.0f;
        private float timeLeft = 0.0f;
        private string strFps = null;

        void Awake()
        {
            fpsText = GetComponent<Text>();
        }

        void Update()
        {
            if (fpsText != null)
            {
                ShowFps();
            }
        }

        private void ShowFps()
        {
            timeLeft -= Time.unscaledDeltaTime;

            if (timeLeft <= 0.0)
            {
                float fps = PXR_Plugin.System.UPxr_GetConfigInt(ConfigType.RenderFPS);
                
                strFps = string.Format("FPS: {0:f0}", fps);
                fpsText.text = strFps;

                timeLeft += updateInterval;
            }
        }
    }
}