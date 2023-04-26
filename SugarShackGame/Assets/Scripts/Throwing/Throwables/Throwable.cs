using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Throwable : MonoBehaviour, IFlow, ThrowableFactoryPool.IPoolable, IThrowable
{
    public ThrowableData data;
    [HideInInspector]
    public CountDownTimer timer;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public bool readyToBeDestroyed = false;
    public float rotationSpeed = 10f;

    private Thrower thrower = null;

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
        thrower = null;
    }

    public void Deactivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }

    public virtual void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        readyToBeDestroyed = false;
        timer = new CountDownTimer(data.TimeBeforeDestruction, false);
        timer.OnTimeIsUpLogic = () => { readyToBeDestroyed = true; };
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
        Debug.Log(thrower);
    }

    public AbilityType GetThrowableType()
    {
        return data.type;
    }

    public virtual void Throw(Vector3 velocity, bool addRotation = false, bool isGhost = false)
    {
        timer.StartTimer();
        gameObject.transform.SetParent(null);
        rb.isKinematic = false;

        rb.AddForce(velocity, ForceMode.Impulse);

        if (addRotation)
        {
            rb.AddTorque(Random.insideUnitSphere * rotationSpeed);
        }
    }

    public virtual void AttachToThrower(Thrower _thrower)
    {
        thrower = _thrower;
        gameObject.transform.position = thrower.attachPoint.position;
        gameObject.transform.SetParent(thrower.attachPoint);

        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Thrower collided = collision.gameObject.GetComponentInParent<Thrower>();
        if (collided != thrower || collided == null)
        {
            Ragdoll ragdollComponent = collision.collider.GetComponentInParent<Ragdoll>();

            if (ragdollComponent != null)
            {
                Vector3 force = rb.velocity;
                ragdollComponent.ragdollTrigger.Invoke(collision.GetContact(0).point, force);
            }
        }
    }

    public virtual void OnThrowLogic(Vector3 velocity)
    {
        
    }
}

