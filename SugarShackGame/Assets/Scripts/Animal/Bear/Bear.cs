using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    public override void PreInitialize()
    {
        base.PreInitialize();
    }
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void Refresh()
    {
        base.Refresh();
    }

    public override void PhysicsRefresh()
    {
        base.PhysicsRefresh();
    }

    public override bool IsPatrolFinished()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {

            Debug.Log("Bear special transition");
            return true;
        }
        else
            return false;
    }
}
