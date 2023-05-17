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

        MakeEffect();
    }

    public override void MakeEffect()
    {
        base.MakeEffect();

        GameObject shieldObj = Instantiate<GameObject>(ShieldPrefab);
        var effect = shieldObj.GetComponent<ShieldEffect>();
        effect.ProtectPlayer(player, ShieldEffectDuration);
    }
}
