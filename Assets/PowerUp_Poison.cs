using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Poison : PowerUpBase
{

    public override void PowerUpEffect(GameObject player)
    {
        player.transform.GetChild(2).GetChild(0).GetComponent<TongueFX>().effectOn(TongueFX.effectTypes.poison);
    }
}
