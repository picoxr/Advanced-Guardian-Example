using UnityEngine;
using Unity.XR.PXR;
using UnityEngine.UI;

public class DebugPoseOutput : MonoBehaviour
{
    public Text unityPose;
    public Text devicePose;

    PxrSensorState2 pxrSensorState;
    int frame;
    Vector3 _devicePose;

    void Update()
    {
        PXR_Plugin.System.UPxr_GetPredictedMainSensorStateNew(ref pxrSensorState, ref frame);
        _devicePose.x = pxrSensorState.globalPose.position.x;
        _devicePose.y = pxrSensorState.globalPose.position.y;
        _devicePose.z = -pxrSensorState.globalPose.position.z;
        //DevicePose.text = _DevicePose.ToString();
        devicePose.text = "_____";

        unityPose.text = Camera.main.transform.position.ToString("#0.000");
    }
}
