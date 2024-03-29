﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    
    #region variables   
    //private int currentHealth;
    public int currentHealth;
    public int maxhealth;
    private Rigidbody2D RB;
    public GameObject DeathPrefab;
    public GameObject healthBar;
    public Slider slider;
    private bool takingDOT;
    private int DOTamount;
    private int DOTinterval;
    private int DOTtimer;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        slider.value = CalculateHealth();

        RB = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        if(takingDOT)
        {
            if(DOTtimer > 0)
            {
                DOTtimer--;
            }
            else
            {
                DOTtimer = DOTinterval;
                takeDamage(DOTamount);
            }
        }
    }
    public void takeDamage(int dmg, Vector3 dmgSource)
    {
        currentHealth -= dmg;
        slider.value = CalculateHealth();
        knockBack(dmg, dmgSource);
        if (currentHealth < maxhealth)
        {
            healthBar.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            Death();
        }

    }
    public void takeDamage(int dmg)
    {
        currentHealth -= dmg;
        slider.value = CalculateHealth();
        if (currentHealth < maxhealth)
        {
            healthBar.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            Death();
        }

    }
    void knockBack(float amount, Vector3 impactSource)
    {
        RB.AddForce((transform.position - impactSource).normalized * amount*5, ForceMode2D.Impulse);
    }
    private void Death()
    {
        GameObject guts = Instantiate(DeathPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    float CalculateHealth()
    {
        //Debug.Log("Current health: " + currentHealth);
        return currentHealth;
    }

    public void initDOT(int amount, int interval)
    {
        takingDOT = true;
        DOTamount = amount;
        DOTinterval = interval;
        GetComponent<SpriteRenderer>().color = Color.magenta;
    }
}
