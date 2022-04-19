using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
using UnityEngine.UI;

public class FindSensorOrigin : MonoBehaviour
{
    public Text ADoriginPos;
    Matrix4x4 TransMatrix;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    PxrSensorState2 _pxrSensorState;
    int frame;
    Vector3 _globalPose;
    Quaternion _quaternion;
    // Update is called once per frame
    void Update()
    {
        PXR_Plugin.System.UPxr_GetPredictedMainSensorStateNew(ref _pxrSensorState, ref frame);
        _globalPose.x = _pxrSensorState.globalPose.position.x;
        _globalPose.y = _pxrSensorState.globalPose.position.y;
        _globalPose.z = -_pxrSensorState.globalPose.position.z;

        _quaternion.w = _pxrSensorState.globalPose.orientation.w;
        _quaternion.x = _pxrSensorState.globalPose.orientation.x;
        _quaternion.y = _pxrSensorState.globalPose.orientation.y;
        _quaternion.z = _pxrSensorState.globalPose.orientation.z;

        TransMatrix = Matrix4x4.TRS(_globalPose, _quaternion, new Vector3(1, 1, 1));
        ADoriginPos.text = "ADOrigin"+TransMatrix.MultiplyPoint(Vector3.zero).ToString();
        gameObject.transform.position = TransMatrix.MultiplyPoint(Vector3.zero);
    }
}
