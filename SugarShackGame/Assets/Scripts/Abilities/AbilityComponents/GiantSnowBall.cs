using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSnowBall : ThrowableAbility
{
    //Todo put it in snow ball slot
    [Header("GiantSnowBall stats")]
    public float impactForceMultplicator = 15;

    public override void OnCollisionLogic(Collision collision)
    {
        Thrower collided = collision.gameObject.GetComponentInParent<Thrower>();
        if (collided != thrower || collided == null)
        {
            Ragdoll ragdollComponent = collision.collider.GetComponentInParent<Ragdoll>();

            if (ragdollComponent != null)
            {
                Vector3 force = rb.velocity;
                ragdollComponent.ragdollTrigger.Invoke(collision.GetContact(0).point, force * impactForceMultplicator);
            }
        }
    }
}
