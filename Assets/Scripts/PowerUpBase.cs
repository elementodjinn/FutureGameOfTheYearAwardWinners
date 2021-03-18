using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag != "Player") return; // terminate immediately if it isnot a player
        PowerUpEffect(collision.gameObject);
        Destroy(gameObject);
    }
    public abstract void PowerUpEffect(GameObject player);

}
