using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlow
{
    public Renderer[] renderers;
    public Animator animController;
    public Ragdoll ragdoll;

    public PlayerController playerController;

    public void PreInitialize()
    {
        ragdoll.PreInitialize();
        playerController.PreInitialize();
    }

    public void Initialize()
    {
        ragdoll.Initialize();
        playerController.Initialize();
    }

    public void Refresh()
    {
        ragdoll.Refresh();

        //animController.SetFloat("Speed", Mathf.Clamp01(playerController.playerSpeed/stats.maxWalkSpeed));

        playerController.Refresh();
    }

    public void PhysicsRefresh()
    {
        ragdoll.PhysicsRefresh();
        playerController.PhysicsRefresh();
    }
}
