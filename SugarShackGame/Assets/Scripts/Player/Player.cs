using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlow
{
    public PlayerStats stats;
    public Renderer[] renderers;
    public Animator animController;

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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animController.SetFloat("Speed", 0.7f);
        }
    }
}
