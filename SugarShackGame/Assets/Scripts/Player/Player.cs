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
        //Allo Tommy
        playerBucket = GetComponent<PlayerBucket>();
        playerBucket = transform.GetComponentInChildren<PlayerBucket>();
        throwerComponent = GetComponent<Thrower>();
        recieverComponent = GetComponent<Reciever>();
        abilityHandler = GetComponent<PlayerAbilityHandler>();
        footStepMaker = GetComponent<PlayerFootStepMaker>();

        ragdoll.PreInitialize();
        playerController.PreInitialize();
        playerBucket.PreInitialize();
        throwerComponent.PreInitialize();
        recieverComponent.PreInitialize();
        abilityHandler.PreInitialize();
        footStepMaker.PreInitialize();

        CauldronManager.Instance.CreateCauldron(this);
        syrupCanManager = new SyrupCanManager();
        playerScore = new PlayerScore(this);
    }

    public void Initialize()
    {
        ragdoll.Initialize();
        playerController.Initialize();
        playerBucket.Initialize();
        throwerComponent.Initialize();
        recieverComponent.Initialize();
        abilityHandler.Initialize();
        footStepMaker.Initialize();
    }

    public void SpawnAtLocation(Vector3 spawnLocation)
    {
        CharacterController ctrl = GetComponent<CharacterController>();

        ctrl.enabled = false;
        transform.position = spawnLocation;
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
        footStepMaker.PhysicsRefresh();
    }
}
