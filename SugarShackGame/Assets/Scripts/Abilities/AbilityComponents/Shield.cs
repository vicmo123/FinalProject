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
        Debug.Log("Protect me");
    }
}
