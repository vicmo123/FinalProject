using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlow
{
    public Renderer[] renderers;

    private Ragdoll ragdoll;
    private PlayerController playerController;
    public PlayerBucket playerBucket { get; private set; }
    private Thrower throwerComponent;

    public void PreInitialize()
    {
        ragdoll = GetComponent<Ragdoll>();
        playerController = GetComponent<PlayerController>();
        playerBucket = GetComponent<PlayerBucket>();
        throwerComponent = GetComponent<Thrower>();

        ragdoll.PreInitialize();
        playerController.PreInitialize();
        playerBucket.PreInitialize();
        throwerComponent.PreInitialize();
        playerBucket = transform.GetComponentInChildren<PlayerBucket>();

        CauldronManager.Instance.CreateCauldron(this);
    }

    public void Initialize()
    {
        ragdoll.Initialize();
        playerController.Initialize();
        playerBucket.Initialize();
        throwerComponent.Initialize();
    }

    public void Refresh()
    {
        ragdoll.Refresh();
        playerController.Refresh();
        playerBucket.Refresh();
        throwerComponent.Refresh();

    }

    public void PhysicsRefresh()
    {
        ragdoll.PhysicsRefresh();
        playerController.PhysicsRefresh();
        playerBucket.PhysicsRefresh();
        throwerComponent.PhysicsRefresh();
    }
}
