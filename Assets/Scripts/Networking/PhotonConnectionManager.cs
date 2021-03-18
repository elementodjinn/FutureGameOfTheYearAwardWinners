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
    public InputField roomNameInput;
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

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.GameVersion = "0.0";
        if(roomNameInput.text == "")
        {
            roomNameInput.text = Time.time.ToString();
        }
        PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 2 },TypedLobby.Default);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 2});
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room you tried to connect is closed");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        PhotonNetwork.LoadLevel("lobby");
    }
    #endregion
}
