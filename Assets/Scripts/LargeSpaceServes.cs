/*******************************************************************************
Copyright © 2015-2022 Pico Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
Pico Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to Pico Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd. 
*******************************************************************************/

using UnityEngine;
using Unity.XR.PXR;

public class LargeSpaceServes : MonoBehaviour
{
    private void Awake()
    {
        PXR_System.InitSystemService(name);
        PXR_System.BindSystemService();
        PXR_System.StartAudioReceiver(name);
        PXR_System.StartBatteryReceiver(name);
    }

    void Update()
    {
        PXR_System.GetSwitchLargeSpaceStatus(str => {
            int x =  int.Parse(str);
            Debug.Log("GetSwitchLargeSpaceStatus x: " + x);
        });
    }

    private void BoolCallback(string value)
    {
        if (PXR_Plugin.System.BoolCallback != null) PXR_Plugin.System.BoolCallback(bool.Parse(value));
        PXR_Plugin.System.BoolCallback = null;
    }
    private void IntCallback(string value)
    {
        if (PXR_Plugin.System.IntCallback != null) PXR_Plugin.System.IntCallback(int.Parse(value));
        PXR_Plugin.System.IntCallback = null;
    }
    private void LongCallback(string value)
    {
        if (PXR_Plugin.System.LongCallback != null) PXR_Plugin.System.LongCallback(int.Parse(value));
        PXR_Plugin.System.LongCallback = null;
    }
    private void StringCallback(string value)
    {
        if (PXR_Plugin.System.StringCallback != null) PXR_Plugin.System.StringCallback(value);
        PXR_Plugin.System.StringCallback = null;
    }
    public void toBServiceBind(string s)
    {
        Debug.Log("Bind success.");
    }
}
