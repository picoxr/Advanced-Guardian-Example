using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Unity.XR.PXR;
using UnityEngine.UI;
using UnityEngine.XR;

public class Player : MonoBehaviourPun
{
    public GameObject HeadCamera;
    public GameObject Avatar;
    public GameObject PosPoint;

    private Transform _body;
    private Transform _headModel;
    //private Transform _leftHand;
    //private Transform _rightHand;

    private Vector3 _headPosition;
    private Vector3 _headRotation;

    //private Vector3 _rightHandPos;
    //private Vector3 _leftHandPos;

    //private Quaternion _rightHandRot;
    //private Quaternion _leftHandRot;

    public float HeadHeight = 1.3f;
    public float bodyHeight = 0.687f;

    private void Awake()
    {
        //Init
        _body = Avatar.transform.Find("Body");
        _headModel = Avatar.transform.Find("Head");
        HeadHeight = _headModel.transform.position.y;
        bodyHeight = _body.transform.position.y;
    }

    void Start()
    {
        if (!photonView.IsMine)
        {
            HeadCamera.SetActive(false);
        }
        else
        {
            SetChildrenLayer(Avatar.transform, "LocalPlayer");
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Head
            _headPosition.x = Camera.main.transform.position.x;
            _headPosition.z = Camera.main.transform.position.z;
            _headRotation = Camera.main.transform.rotation.eulerAngles;

            _headModel.position = new Vector3(_headPosition.x, HeadHeight, _headPosition.z);
            _headModel.rotation = Quaternion.Euler(_headRotation);

            // Body
            _body.transform.position = new Vector3(_headPosition.x, bodyHeight, _headPosition.z);

            // Hand
            //InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.devicePosition, out _leftHandPos);
            // InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.devicePosition, out _rightHandPos);
            //PosPoint
            PosPoint.transform.position = new Vector3(_headPosition.x, 0, _headPosition.z);
        }
    }

    private void SetChildrenLayer(Transform trans, string layerName)
    {
        Transform[] _trans = trans.GetComponentsInChildren<Transform>();
        foreach (Transform value in _trans)
        {
            value.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }
}
