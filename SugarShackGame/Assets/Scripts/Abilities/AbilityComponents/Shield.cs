using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : UnThrowableAbility
{
    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        MakeEffect();
    }

    public override void MakeEffect()
    {
        //Todo switch power-ups

        //var players = GameObject.FindGameObjectsWithTag("Player");

        //Player p1 = players[0].GetComponent<Player>();
        //Player p2 = players[1].GetComponent<Player>();

        //p1.abilityHandler.abilitySlots[]
    }
}
