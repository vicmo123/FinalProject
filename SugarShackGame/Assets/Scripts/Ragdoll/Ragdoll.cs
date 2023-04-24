using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;

public class Ragdoll : MonoBehaviour, IFlow
{
    //Ragdoll
    private List<RagdollBodyPart> partsList;
    public Action<Vector3, Vector3> ragdollTrigger;

    //To Deactivate
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;

    //Recovery
    public float lerpSpeed = 5f;
    public float timeBeforeRecovery = 2f;
    private CountDownTimer timer;


    public void PreInitialize()
    {
        timer = new CountDownTimer(timeBeforeRecovery, false);
        timer.OnTimeIsUpLogic += () => { Recover(); };

        ragdollTrigger = (hitPoint, hitForce) => { TriggerRagdoll(hitPoint, hitForce); };

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Initialize()
    {
        partsList = new List<RagdollBodyPart>();
        Rigidbody[] ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in ragdollRigidbodies)
        {
            if (!rb.gameObject.CompareTag("Animal"))
                partsList.Add(new RagdollBodyPart(rb, rb.transform.localPosition, rb.transform.localRotation));
        }

        DisableRagdoll();
    }

    public void Refresh()
    {
        timer.UpdateTimer();
    }

    public void PhysicsRefresh()
    {

    }

    private void TriggerRagdoll(Vector3 hitPoint, Vector3 hitForce)
    {
        RecordAllPositionAndRotation();
        EnableRagdoll();

        Rigidbody hitRigidbody = partsList.OrderBy(part => Vector3.Distance(part.rb.position, hitPoint)).First().rb;
        hitRigidbody.AddForceAtPosition(hitForce, hitPoint, ForceMode.Impulse);

        timer.StartTimer();
    }

    private void EnableRagdoll()
    {
        foreach (var part in partsList)
        {
            part.rb.isKinematic = false;
        }

        if (animator)
            animator.enabled = false;
        if (characterController)
            characterController.enabled = false;
        if (agent)
            agent.enabled = false;
    }

    private void DisableRagdoll()
    {
        foreach (var part in partsList)
        {
            part.rb.isKinematic = true;
        }

        if (animator)
            animator.enabled = true;
        if (characterController)
            characterController.enabled = true;
        if (agent)
            agent.enabled = true;
    }

    public void Recover()
    {
        DisableRagdoll();
        foreach (var ragdollPart in partsList)
        {
            ragdollPart.rb.velocity = Vector3.zero;
            ragdollPart.rb.angularVelocity = Vector3.zero;
            ragdollPart.rb.ResetInertiaTensor();
            StartCoroutine(LerpToDefaultPosition(ragdollPart));
        }
    }

    private IEnumerator LerpToDefaultPosition(RagdollBodyPart ragdollPart)
    {
        float t = 0.0f;
        Vector3 startPos = ragdollPart.rb.transform.localPosition;
        Quaternion startRot = ragdollPart.rb.transform.localRotation;
        Vector3 endPos = ragdollPart.defaultPosition;
        Quaternion endRot = ragdollPart.defaultRotation;

        while (t < 1.0f)
        {
            t += Time.deltaTime * lerpSpeed;
            t = Mathf.Clamp01(t);
            ragdollPart.rb.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            ragdollPart.rb.transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        ragdollPart.rb.transform.localPosition = endPos;
        ragdollPart.rb.transform.localRotation = endRot;

        ragdollPart.SetCurrentPositionAndRotation();
    }

    private void RecordAllPositionAndRotation()
    {
        foreach (var part in partsList)
        {
            part.SetCurrentPositionAndRotation();
        }
    }

    public struct RagdollBodyPart
    {
        public Rigidbody rb;
        public Vector3 defaultPosition;
        public Quaternion defaultRotation;

        public RagdollBodyPart(Rigidbody _rb, Vector3 _defaultPosition, Quaternion _defaultRotation)
        {
            rb = _rb;
            defaultPosition = _defaultPosition;
            defaultRotation = _defaultRotation;
        }

        public void SetCurrentPositionAndRotation()
        {
            defaultPosition = rb.transform.localPosition;
            defaultRotation = rb.transform.localRotation;
        }
    }
}
