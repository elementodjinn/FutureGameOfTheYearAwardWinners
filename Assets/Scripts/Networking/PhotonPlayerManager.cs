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
    public GameObject camPrefab;
    private GameObject playerInstance;
    [SerializeField] private GameObject startButtton;
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
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity,0);
        GameObject cam = Instantiate(camPrefab, playerInstance.transform);
        playerInstance.transform.Find("tongue").GetChild(0).GetComponent<TongueControl>().cam = cam.GetComponent<Camera>();
        DontDestroyOnLoad(playerInstance);
        Debug.Log(PhotonNetwork.IsMasterClient);
        if(PhotonNetwork.IsMasterClient)
        {
            startButtton.SetActive(true);
        }
        //playerInstance.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PhotonNetwork.NickName;
    }

}
