using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : IFlow
{
    public AnimalData data;
    public virtual void PreInitialize()
    {
    }

    public virtual void Initialize()
    {
        Debug.Log("Animal initialize");
    }

    public virtual void Refresh()
    {
    }

    public virtual void PhysicsRefresh()
    {
    }
}
