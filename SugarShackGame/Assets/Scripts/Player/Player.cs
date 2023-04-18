using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlow
{
    public PlayerStats stats;
    public Renderer[] renderers;
    public Animator animController;
    public Ragdoll ragdoll;

    public void PreInitialize()
    {
        ragdoll.PreInitialize();
    }

    public void Initialize()
    {
        ragdoll.Initialize();
    }

    public void Refresh()
    {
        ragdoll.Refresh();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animController.SetFloat("Speed", 0.7f);
        }
    }

    public void PhysicsRefresh()
    {
        ragdoll.PhysicsRefresh();
    }
}
