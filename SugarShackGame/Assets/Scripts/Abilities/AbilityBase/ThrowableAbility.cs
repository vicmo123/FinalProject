using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAbility : AbilityComponent
{
    [HideInInspector]
    public Rigidbody rb;
    public float rotationSpeed = 10f;
    public float speed = 10f;
    public float maxSpeed = 50f;
    private Thrower thrower = null;


    public override void Initialize()
    {
        base.Initialize();
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(float timeHeld, bool isIfiniteTime = false)
    {
        if (!isIfiniteTime) {
            timer.StartTimer();
        }

        gameObject.transform.SetParent(null);
        rb.isKinematic = false;

        rb.AddForce(thrower.gameObject.transform.forward * ComputeSpeed(timeHeld), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * rotationSpeed);
    }

    public virtual void AttachToThrower(Thrower _thrower)
    {
        thrower = _thrower;
        gameObject.transform.position = thrower.attachPoint.position;
        gameObject.transform.SetParent(thrower.attachPoint);

        rb.isKinematic = true;
        thrower.toThrow = this;
        _thrower.IsHoldingThrowable = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionLogic(collision);
    }

    public virtual void OnCollisionLogic(Collision collision)
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

    public float ComputeSpeed(float timeHeld)
    {
        return Mathf.Clamp(speed * timeHeld, speed, maxSpeed);
    }

    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        AttachToThrower(_player.throwerComponent);
    }
}
