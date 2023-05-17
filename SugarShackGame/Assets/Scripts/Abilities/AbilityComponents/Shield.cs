using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : UnThrowableAbility
{
    public GameObject ShieldPrefab;
    public float ShieldEffectDuration = 8f;

    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        //MakeEffect();
    }

    public override void MakeEffect()
    {
        var iceSphere = GameObject.Instantiate(ShieldPrefab);
        iceSphere.GetComponent<ShieldEffect>().MakeShieldEffect(ShieldEffectDuration, player);
        readyToBeDestroyed = true;
    }
}
