using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlow
{
    public Renderer[] renderers;
    public PlayerBucket playerBucket { get; private set; }
    [HideInInspector]
    public Color color;
    private Ragdoll ragdoll;
    private PlayerController playerController;
    [HideInInspector]
    public Thrower throwerComponent;
    [HideInInspector]
    public Reciever recieverComponent;
    [HideInInspector]
    public PlayerAbilityHandler abilityHandler;
    [HideInInspector]
    public FloatingPointsHandler floatingPointHandler;
    [HideInInspector]
    public SyrupCanManager syrupCanManager;
    [HideInInspector]
    public PlayerFootStepMaker footStepMaker;
    [HideInInspector]
    public PlayerScore playerScore;
    public int index;

    public void PreInitialize()
    {

        ragdoll = GetComponent<Ragdoll>();
        playerController = GetComponent<PlayerController>();
        playerBucket = transform.GetComponentInChildren<PlayerBucket>();

        throwerComponent = GetComponent<Thrower>();
        recieverComponent = GetComponent<Reciever>();
        abilityHandler = GetComponent<PlayerAbilityHandler>();
        floatingPointHandler = GetComponentInChildren<FloatingPointsHandler>();
        footStepMaker = GetComponent<PlayerFootStepMaker>();

        ragdoll.PreInitialize();
        playerController.PreInitialize();
        playerBucket.PreInitialize();
        throwerComponent.PreInitialize();
        recieverComponent.PreInitialize();
        abilityHandler.PreInitialize();
        floatingPointHandler.PreInitialize();
        footStepMaker.PreInitialize();

        CauldronManager.Instance.CreateCauldron(this);
        syrupCanManager = new SyrupCanManager();
        playerScore = new PlayerScore(this, floatingPointHandler);
    }

    public void Initialize()
    {
        ragdoll.Initialize();
        playerController.Initialize();
        playerBucket.Initialize();
        throwerComponent.Initialize();
        recieverComponent.Initialize();
        abilityHandler.Initialize();
        floatingPointHandler.Initialize();
        footStepMaker.Initialize();
    }

    public void SpawnAtLocation(Vector3 spawnLocation, Quaternion spawnRotation)
    {
        CharacterController ctrl = GetComponent<CharacterController>();

        ctrl.enabled = false;
        transform.position = spawnLocation;
        transform.rotation = spawnRotation;
        ctrl.enabled = true;
    }

    public void Refresh()
    {
        ragdoll.Refresh();
        playerController.Refresh();
        playerBucket.Refresh();
        throwerComponent.Refresh();
        recieverComponent.Refresh();
        abilityHandler.Refresh();
        floatingPointHandler.Refresh();
        footStepMaker.Refresh();
    }

    public void PhysicsRefresh()
    {
        ragdoll.PhysicsRefresh();
        playerController.PhysicsRefresh();
        playerBucket.PhysicsRefresh();
        throwerComponent.PhysicsRefresh();
        recieverComponent.PhysicsRefresh();
        abilityHandler.PhysicsRefresh();
        floatingPointHandler.PhysicsRefresh();
        footStepMaker.PhysicsRefresh();
    }
}
