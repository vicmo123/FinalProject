using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Throwable : MonoBehaviour, IFlow, ThrowableFactoryPool.IPoolable, IThrowable
{
    [HideInInspector]
    public Transform throwerAttachPoint = null;
    public ThrowableData data;
    public CountDownTimer timer;
    public Rigidbody rb;
    public bool readyToBeDestroyed = false;

    private bool isActive = true;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void Deactivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }

    public virtual void Initialize()
    {
        readyToBeDestroyed = false;
        timer = new CountDownTimer(data.TimeBeforeDestruction, false);
        timer.OnTimeIsUpLogic = ()  => { readyToBeDestroyed = true; };
    }

    public virtual void PreInitialize()
    {
        
    }

    public virtual void Refresh()
    {
        timer.UpdateTimer();
    }

    public virtual void PhysicsRefresh()
    {

    }

    public ThrowableTypes GetThrowableType()
    {
        return data.type;
    }

    public virtual void Throw(Vector3 velocity)
    {
        timer.StartTimer();
        gameObject.transform.SetParent(null);
        rb.isKinematic = false;
    }

    public virtual void AttachToThrower(Thrower thrower)
    {
        throwerAttachPoint = thrower.attachPoint;
        gameObject.transform.position = throwerAttachPoint.position;
        gameObject.transform.SetParent(throwerAttachPoint);

        rb.isKinematic = true;
    }
}

