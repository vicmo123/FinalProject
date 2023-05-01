using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

public class Ragdoll : MonoBehaviour, IFlow
{
    //Ragdoll
    private List<RagdollBodyPart> partsList;
    public Action<Vector3, Vector3> ragdollTrigger;
    public Action<Vector3> ragdollTriggerAll;

    //To Deactivate
    private CustomInputHandler input;
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;
    private PlayerController playerController;

    //Recovery
    public float minLerpSpeed = 0.1f;
    public float maxLerpSpeed = 0.5f;
    public float timeBeforeRecovery = 2f;
    public LayerMask groundMask;
    private bool isInRagdollState = false;
    public float timeBeforeForcingRecovery = 5f;
    private CountDownTimer recoveryTimer;

    public void PreInitialize()
    {
        // this timer is to force the recovery it it has been too long
        recoveryTimer = new CountDownTimer(timeBeforeForcingRecovery, false);
        recoveryTimer.OnTimeIsUpLogic = () =>
        {
            StartCoroutine(Recover());
            isInRagdollState = false;
        };

        ragdollTrigger = (hitPoint, hitForce) => { TriggerRagdoll(hitPoint, hitForce); };
        ragdollTriggerAll = (hitForce) => { TriggerRagdollAll(hitForce); };

        input = GetComponent<CustomInputHandler>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
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
        if(isInRagdollState) {
            bool allSleeping = true;
            foreach (var part in partsList)
            {
                if (!part.rb.IsSleeping())
                {
                    allSleeping = false;
                    break;
                }
            }

            if (allSleeping)
            {
                StartCoroutine(Recover());
                isInRagdollState = false;
            }

        }
        recoveryTimer.UpdateTimer();
    }

    public void PhysicsRefresh()
    {

    }

    private void TriggerRagdoll(Vector3 hitPoint, Vector3 hitForce)
    {
        EnableRagdoll();

        Rigidbody hitRigidbody = partsList.OrderBy(part => Vector3.Distance(part.rb.position, hitPoint)).First().rb;
        hitRigidbody.AddForceAtPosition(hitForce, hitPoint, ForceMode.Impulse);
    }

    private void TriggerRagdollAll(Vector3 hitForce)
    {
        EnableRagdoll();

        foreach (var part in partsList)
        {
            part.rb.AddForce(hitForce, ForceMode.Impulse);
        }
    }

    private void EnableRagdoll()
    {
        foreach (var part in partsList)
        {
            part.rb.isKinematic = false;
        }

        if (input)
        {
            input.BlockControls();
        }
        if (animator)
        {
            animator.enabled = false;
        }
        if (characterController)
        {
            characterController.enabled = false;
        }
        if (agent)
        {
            agent.enabled = false;
        }
        if (playerController)
        {
            playerController.enabled = false;
        }

        isInRagdollState = true;
        recoveryTimer.StartTimer();
    }

    private void DisableRagdoll()
    {
        foreach (var part in partsList)
        {
            part.rb.isKinematic = true;
        }

        if (input)
        {
            input.UnlockControls();
        }
        if (animator)
        {
            animator.enabled = true;
        }
        if (characterController)
        {
            characterController.enabled = true;
        }
        if (agent)
        {
            agent.Warp(transform.position);
            agent.enabled = true;
        }
        if (playerController)
        {
            playerController.enabled = true;
        }
    }

    int numCoroutines = 0;
    private IEnumerator Recover()
    {
        numCoroutines = 0;

        foreach (var ragdollPart in partsList)
        {
            Debug.Log(ragdollPart.rb.gameObject.name);
            ragdollPart.rb.isKinematic = true;
            ragdollPart.rb.velocity = Vector3.zero;
            ragdollPart.rb.angularVelocity = Vector3.zero;
            ragdollPart.rb.ResetInertiaTensor();

            numCoroutines++;
            StartCoroutine(LerpToDefaultPosition(ragdollPart));
        }

        yield return new WaitUntil(() => numCoroutines == 0);

        if (input)
        {
            input.UnlockControls();
        }
        if (animator)
        {
            animator.enabled = true;
        }
        if (characterController)
        {
            characterController.enabled = true;
        }
        if (agent)
        {
            agent.Warp(transform.position);
            agent.enabled = true;
        }
        if (playerController)
        {
            playerController.enabled = true;
        }
    }

    private IEnumerator LerpToDefaultPosition(RagdollBodyPart ragdollPart)
    {

        float t = 0.0f;
        Vector3 startPos = ragdollPart.rb.transform.localPosition;
        Quaternion startRot = ragdollPart.rb.transform.localRotation;
        Vector3 endPos = ragdollPart.defaultPosition;
        Quaternion endRot = ragdollPart.defaultRotation;

        float randLerpSpeed = Random.Range(minLerpSpeed, maxLerpSpeed);

        while (t < 1.0f)
        {
            t += Time.deltaTime * randLerpSpeed;
            t = Mathf.Clamp01(t);
            ragdollPart.rb.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            ragdollPart.rb.transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        ragdollPart.rb.transform.localPosition = endPos;
        ragdollPart.rb.transform.localRotation = endRot;

        numCoroutines--;
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
