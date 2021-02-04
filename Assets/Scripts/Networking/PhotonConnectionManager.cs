using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PhotonConnectionManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    #region variables
    private string playerName;
    public InputField playerNameInput;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame

    public void Connect()
    {
        playerName = playerNameInput.text;
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.ConnectUsingSettings();

    }

    #region PUN callbacks

    private void OnConnectedToServer()
    {
        Debug.Log("Connected To server");
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.GameVersion = "0.0";
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 2});
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        PhotonNetwork.LoadLevel("Sin Scene");
    }
    #endregion
}
