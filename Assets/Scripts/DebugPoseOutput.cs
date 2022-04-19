using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
using UnityEngine.UI;

public class DebugPoseOutput : MonoBehaviour
{

   public Text UnityPose;
   public Text DevicePose;

    PxrSensorState2 _pxrSensorState;
    int frame;
    Vector3 _DevicePose;

    void Update()
    {
        PXR_Plugin.System.UPxr_GetPredictedMainSensorStateNew(ref _pxrSensorState, ref frame);
        _DevicePose.x = _pxrSensorState.globalPose.position.x;
        _DevicePose.y = _pxrSensorState.globalPose.position.y;
        _DevicePose.z = -_pxrSensorState.globalPose.position.z;
        //DevicePose.text = _DevicePose.ToString();
        DevicePose.text = "_____";

        UnityPose.text = Camera.main.transform.position.ToString("#0.000");

    }
}
