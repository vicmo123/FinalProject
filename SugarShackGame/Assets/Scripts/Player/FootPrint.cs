using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour, IFlow
{
    public CountDownTimer timer;
    public float timeBeforeDestruction = 10f;
    public SpriteRenderer SpriteRenderer;

    public bool isReadyToBeDestoryed = false;

    public void Initialize()
    {
        timer = new CountDownTimer(timeBeforeDestruction, true);
    }

    public void PreInitialize()
    {
    }

    public void PhysicsRefresh()
    {
    }


    public void Refresh()
    {
        timer.UpdateTimer();
    }
}
