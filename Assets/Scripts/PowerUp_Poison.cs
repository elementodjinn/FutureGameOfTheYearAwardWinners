using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Poison : PowerUpBase
{

    public override void PowerUpEffect(GameObject player)
    {
        Transform tongueTip = player.transform.GetChild(2).GetChild(0);
        tongueTip.GetComponent<TongueFX>().effectOn(TongueFX.effectTypes.poison);
        tongueTip.GetComponent<TongueControl>().poison = true;
    }
}
