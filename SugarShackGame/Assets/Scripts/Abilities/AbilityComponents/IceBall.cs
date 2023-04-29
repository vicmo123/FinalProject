using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : ThrowableAbility
{
    public GameObject iceSpherePrefab;

    public override void OnCollisionLogic(Collision collision)
    {
        Thrower collided = collision.gameObject.GetComponentInParent<Thrower>();
        if (collided != thrower || collided == null)
        {
            if (collision.transform.root.CompareTag("Player") || collision.transform.root.CompareTag("Animal"))
            {
                var iceSphere = GameObject.Instantiate(iceSpherePrefab);
                iceSphere.GetComponent<IceEffect>().FreezeEffect(collision.transform.root.gameObject);
                readyToBeDestroyed = true;
            }
        }
    }

    
}
