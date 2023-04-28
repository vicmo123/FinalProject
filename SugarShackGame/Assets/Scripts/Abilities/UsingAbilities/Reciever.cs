using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour, IFlow
{
    [Header("Throwing Settings")]
    public Transform attachPoint;
    [HideInInspector]
    public bool IsHoldingUnThrowable = false;
    [HideInInspector]
    public UnThrowableAbility toUse = null;

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }

    public void PreInitialize()
    {
    }

    public void Refresh()
    {
    }
}
