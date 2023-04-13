using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AbilityState { Ready, Active}
public class AbilityHolder : MonoBehaviour, IFlow
{
    Ability ability;
    AbilityState state;
    UnityEvent triggerAbility;

    public void PreInitialize()
    {
    }

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }

     public void Refresh()
    {
    }
}
