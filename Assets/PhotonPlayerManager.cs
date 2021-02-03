using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonPlayerManager : MonoBehaviour
{
    #region variables
    static PhotonPlayerManager PM;
    private PhotonView PV;
    public GameObject playerPrefab;
    private GameObject playerInstance;

    #endregion

    private void Awake()
    {
        if(PM == null)
        {
            PM = this;
        }
        else if(PM != this)
        {
            Destroy(PM);
            PM = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity,0);
        //playerInstance.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
