using UnityEngine;
using Unity.XR.PXR;
using UnityEngine.UI;

public class FindSensorOrigin : MonoBehaviour
{
    public Text ADoriginPos;
    private Matrix4x4 TransMatrix;
    private PxrSensorState2 pxrSensorState;
    private int frame;
    private Vector3 globalPose;
    private Quaternion quaternion;

    // Update is called once per frame
    void Update()
    {
        PXR_Plugin.System.UPxr_GetPredictedMainSensorStateNew(ref pxrSensorState, ref frame);
        globalPose.x = pxrSensorState.globalPose.position.x;
        globalPose.y = pxrSensorState.globalPose.position.y;
        globalPose.z = -pxrSensorState.globalPose.position.z;

        quaternion.w = pxrSensorState.globalPose.orientation.w;
        quaternion.x = pxrSensorState.globalPose.orientation.x;
        quaternion.y = pxrSensorState.globalPose.orientation.y;
        quaternion.z = pxrSensorState.globalPose.orientation.z;

        TransMatrix = Matrix4x4.TRS(globalPose, quaternion, Vector3.one);
        ADoriginPos.text = "ADOrigin" + TransMatrix.MultiplyPoint(Vector3.zero).ToString();
        gameObject.transform.position = TransMatrix.MultiplyPoint(Vector3.zero);
    }
}
