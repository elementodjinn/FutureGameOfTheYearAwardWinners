using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
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
        if (collision.tag != "Player") return; // terminate immediately if it isnot a player
        PowerUpEffect(collision.gameObject);
        Destroy(gameObject);
    }
    public virtual void PowerUpEffect(GameObject player)    {    }

}
