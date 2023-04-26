using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlow
{
    public Renderer[] renderers;
    public PlayerBucket playerBucket { get; private set; }

    private Ragdoll ragdoll;
    private PlayerController playerController;
    
    [HideInInspector]
    public Thrower throwerComponent;
    [HideInInspector]
    public Reciever recieverComponent;
    private PlayerAbilityHandler abilityHander;

    public void PreInitialize()
    {
        ragdoll = GetComponent<Ragdoll>();
        playerController = GetComponent<PlayerController>();
        playerBucket = GetComponent<PlayerBucket>();
        throwerComponent = GetComponent<Thrower>();
        recieverComponent = GetComponent<Reciever>();
        abilityHander = GetComponent<PlayerAbilityHandler>();

        ragdoll.PreInitialize();
        playerController.PreInitialize();
        playerBucket.PreInitialize();
        //throwerComponent.PreInitialize();
        recieverComponent.PreInitialize();
        abilityHander.PreInitialize();

        playerBucket = transform.GetComponentInChildren<PlayerBucket>();
        CauldronManager.Instance.CreateCauldron(this);
    }

    public void Initialize()
    {
        ragdoll.Initialize();
        playerController.Initialize();
        playerBucket.Initialize();
        throwerComponent.Initialize();
        recieverComponent.Initialize();
        abilityHander.Initialize();
    }

    public void Refresh()
    {
        ragdoll.Refresh();
        playerController.Refresh();
        playerBucket.Refresh();
        throwerComponent.Refresh();
        recieverComponent.Refresh();
        abilityHander.Refresh();
    }

    public void PhysicsRefresh()
    {
        ragdoll.PhysicsRefresh();
        playerController.PhysicsRefresh();
        playerBucket.PhysicsRefresh();
        throwerComponent.PhysicsRefresh();
        recieverComponent.PhysicsRefresh();
        abilityHander.PhysicsRefresh();
    }
}
