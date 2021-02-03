using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    #region variables
    [SerializeField]
    private int healthMax;
    private PhotonView PV;
    public int healthCurrent { get; set; }
    public bool isDead { get; set; }
    #endregion


    #region MonoBehaviour callbacks

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        healthCurrent = healthMax;
        if (PV.IsMine)
        {
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PhotonNetwork.NickName;
        }
        else
        {

            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PhotonNetwork.PlayerListOthers[0].NickName;
        }
    }
    #endregion


    

    public void takeDamage(int amount)
    {
        healthCurrent -= amount;
        if(healthCurrent <= 0)
        {
            onDeath();
        }
    }

    private void onDeath()
    {
        isDead = true;
        ///add more stuff here
    }
}
