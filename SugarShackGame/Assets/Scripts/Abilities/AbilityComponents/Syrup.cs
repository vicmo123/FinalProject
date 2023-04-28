using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syrup : UnThrowableAbility
{
    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        player.syrupCanManager.AddCan();

        Debug.Log(player.syrupCanManager.GetCanCount());
    }
}
