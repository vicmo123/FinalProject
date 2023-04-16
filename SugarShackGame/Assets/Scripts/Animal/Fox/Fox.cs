using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
    public override void PreInitialize()
    {
        base.PreInitialize();
    }
    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("I am a fox");
    }
    public override void Refresh()
    {
        base.Refresh();
    }

    public override void PhysicsRefresh()
    {
        base.PhysicsRefresh();
    }

    public override void OnChaseLogic()
    {
        chaseTarget = null;
    }
}
