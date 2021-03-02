using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_MoveSpeed : PowerUpBase
{
    [SerializeField] float increment = 0.5f;
    public override void PowerUpEffect(GameObject player)
    {
        Debug.Log("MoveSpeed was picked up");
        PlayerMovement PM = player.GetComponent<PlayerMovement>();
        PM.speed += increment;
    }
}
