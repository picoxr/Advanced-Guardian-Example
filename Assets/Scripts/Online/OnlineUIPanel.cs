using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
using Photon.Pun;

[RequireComponent(typeof(Canvas))]
public class OnlineUIPanel : MonoBehaviour
{
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.worldCamera != null)
            return;
        else
        {
             canvas.worldCamera = Camera.main;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        PhotonNetwork.Disconnect();
    }
}
