using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Room : MonoBehaviour
{
    public bool northExit = false; //{ get; set; }
    public bool southExit = false; //{ get; set; }
    public bool eastExit = false; //{ get; set; }
    public bool westExit = false; //{ get; set; }

    [SerializeField]
    public int width { get; set; } = 16;
    [SerializeField]
    public int height { get; set; } = 16;

    public Transform cameraPosition;
    public Transform[] enemySpawnPoints;
    public GameObject enemy;
    public Light roomLight;
    private bool isOn = false;
    private bool spawnedEnemies = false;
    private List<GameObject> playersInside = new List<GameObject>();
    private List<GameObject> aliveEnemies = new List<GameObject>();
    public RoomType roomType { get; set; }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isOn && collision.gameObject.tag == "Player")
        {
            roomLight.enabled = true;
            isOn = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isOn && collision.gameObject.tag == "Player")
        {
            roomLight.enabled = true;
            isOn = true;
            Debug.Log(collision.gameObject);
            playersInside.Add(collision.gameObject);
            if(!spawnedEnemies && PhotonNetwork.IsMasterClient && roomType != RoomType.SpawnRoom)
            {
                foreach(Transform t in enemySpawnPoints)
                {
                    GameObject aliveEnemy = Instantiate(enemy, t.position, t.rotation);
                    aliveEnemies.Add(aliveEnemy);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(playersInside.Count);
        if (collision.gameObject.tag == "Player" && playersInside.Count <= 1)
        {
            roomLight.enabled = false;
            isOn = false;
        }
        playersInside.Remove(collision.gameObject);
    }
}
