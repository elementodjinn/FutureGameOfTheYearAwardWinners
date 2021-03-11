using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Light light;
    private bool isOn = false;
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
        if(!isOn && collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            light.enabled = true;
            isOn = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            light.enabled = true;
            isOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            light.enabled = false;
            isOn = false;
        }
    }
}
