using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Ragdoll : MonoBehaviour, IFlow
{
    private Rigidbody[] ragdollRigidbodies;
    public Animator animator;
    public CharacterController characterController;
    public Action<Vector3, Vector3> ragdollTrigger;

    public void PreInitialize()
    {
        ragdollTrigger = (hitPoint, hitForce) => { TriggerRagdoll(hitPoint, hitForce); };

        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        DisableRagdoll();
    }

    public void Initialize()
    {

    }

    public void Refresh()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    EnableRagdoll();
        //}
    }

    public void PhysicsRefresh()
    {

    }

    private void TriggerRagdoll(Vector3 hitPoint, Vector3 hitForce)
    {
        EnableRagdoll();
        Rigidbody hitRigidbody = ragdollRigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPoint)).First();
        hitRigidbody.AddForceAtPosition(hitForce, hitPoint, ForceMode.Impulse);
    }

    private void EnableRagdoll()
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }

        if(animator)
            animator.enabled = false;
        if(characterController)
            characterController.enabled = false;
    }

    private void DisableRagdoll()
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }

        if (animator)
            animator.enabled = true;
        if (characterController)
            characterController.enabled = true;
    }
}