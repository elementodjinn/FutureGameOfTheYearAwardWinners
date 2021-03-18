using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class lobbyManager : MonoBehaviour
{

    public void onClicked()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;

        PhotonNetwork.LoadLevel("Sin Scene");
    }

}
