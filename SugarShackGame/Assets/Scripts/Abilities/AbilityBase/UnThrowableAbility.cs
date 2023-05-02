using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnThrowableAbility : AbilityComponent
{
    protected Reciever reciever = null;
    [HideInInspector]
    public Rigidbody rb;

    public override void Initialize()
    {
        base.Initialize();
        rb = GetComponent<Rigidbody>();

        timer.OnTimeIsUpLogic += () =>
        {
            reciever.toUse = null;
            reciever.IsHoldingUnThrowable = false;
            reciever = null;
        };
    }

    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        AttachToReciever(_player.recieverComponent);
    }

    private void AttachToReciever(Reciever _reciever)
    {
        reciever = _reciever;
        gameObject.transform.position = reciever.attachPoint.position;
        gameObject.transform.SetParent(reciever.attachPoint);

        rb.isKinematic = true;
        reciever.toUse = this;
        reciever.IsHoldingUnThrowable = true;

        timer.StartTimer();
    }
}
