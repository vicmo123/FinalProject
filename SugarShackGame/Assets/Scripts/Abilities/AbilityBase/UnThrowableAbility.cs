using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnThrowableAbility : AbilityComponent
{
    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        AttachToReciever(_player.recieverComponent);
    }

    private void AttachToReciever(Reciever reciever)
    {
        gameObject.transform.position = Vector3.zero;
        timer.StartTimer();
    }

    private void Use()
    {

    }
}
