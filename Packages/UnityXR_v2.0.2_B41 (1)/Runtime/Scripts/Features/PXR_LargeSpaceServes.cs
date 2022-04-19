using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.PXR;


public class PXR_LargeSpaceServes : MonoBehaviour
{
    private string gameOBJ = "XR Rig";

    private void Awake()
    {
        
        PXR_System.InitSystemService(gameOBJ);
        PXR_System.BindSystemService();
        PXR_System.StartAudioReceiver(gameOBJ);
        PXR_System.StartBatteryReceiver(gameOBJ);
    }

    void Start()
    {

    }


    void Update()
    {
        PXR_System.GetSwitchLargeSpaceStatus(str=> {
            int x =  int.Parse(str);
            if (x == 1)
            {
                PXR_Plugin.System.UPxr_SetLargeSpaceStatus(true);
            }
            else {
                PXR_Plugin.System.UPxr_SetLargeSpaceStatus(false);
            }
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
