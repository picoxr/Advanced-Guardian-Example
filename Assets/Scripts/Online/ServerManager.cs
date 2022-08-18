using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviourPunCallbacks
{
    private readonly byte maxPlayersPerRoom = 6;
    public static ServerManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        DebugHelper.Instance.Log("Connect Success !");
        PhotonNetwork.JoinRandomRoom();
        DebugHelper.Instance.Log("Joining Room...");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        DebugHelper.Instance.Log("No Room exists, create a new room");
        PhotonNetwork.CreateRoom("Advanced Guardian", new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
        SceneManager.LoadScene("001-Main");
        DebugHelper.Instance.Log("DisConnected");
    }

    public override void OnJoinedRoom()
    {
        DebugHelper.Instance.Log("Joined!");
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("002-Sync");
    }
}
