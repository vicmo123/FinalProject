using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : ThrowableAbility
{
    public GameObject trampolinePrefab;
    public LayerMask collisionMask;

    public override void OnCollisionLogic(Collision collision)
    {
        if (collisionMask == (collisionMask | (1 << collision.gameObject.layer)))
        {
            var trampolineObj = GameObject.Instantiate(trampolinePrefab);
            trampolineObj.transform.position = gameObject.transform.position;

            Vector3 normal = collision.contacts[0].normal;
            float angle = Vector3.Angle(Vector3.up, normal);
            Vector3 rotation = new Vector3(0, 0, angle);

            trampolineObj.transform.rotation = Quaternion.Euler(rotation);
            trampolineObj.GetComponent<Rigidbody>().isKinematic = true;

            trampolineObj.GetComponent<TrampolineEffect>().Init();

            readyToBeDestroyed = true;
        }
    }
}
