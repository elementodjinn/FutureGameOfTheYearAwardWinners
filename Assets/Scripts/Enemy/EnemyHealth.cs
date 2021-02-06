using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    #region variables   
    private int currentHealth;
    public int maxhealth;
    private Rigidbody2D RB;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        RB = GetComponent<Rigidbody2D>();

    }

    public void takeDamage(int dmg, Vector3 dmgSource)
    {
        Debug.Log("Take Damage function was called");
        currentHealth -= dmg;
        knockBack(dmg,dmgSource);
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void knockBack(float amount, Vector3 impactSource)
    {
        RB.AddForce((transform.position - impactSource).normalized * amount*3, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
