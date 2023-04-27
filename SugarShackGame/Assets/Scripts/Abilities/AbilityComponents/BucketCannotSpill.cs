using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketCannotSpill : UnThrowableAbility
{
    public float noSpillDuration = 15f; 
    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        MakeEffect();
    }

    public override void MakeEffect()
    {
        StartCoroutine(player.playerBucket.NoSpill(noSpillDuration));
    }
}
