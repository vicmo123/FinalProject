using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour, IFlow
{
    public AnimalStats stats;
    public NavMeshAgent agent;
    public AnimalStateMachine stateMachine;
    public GameObject chaseTarget = null;
    public Animator animController;
    public Ragdoll ragdoll;

    [HideInInspector]
    public bool isScared = false;

    [Header("Adapt To Slop Angle")]
    public float maxRayDist = 1.5f;
    public float slopeRotChangeSpeed = 10f;
    [HideInInspector]
    public Vector3 offset = Vector3.zero;

    public void SpawnAtPosition(Vector3 spawnPos)
    {
        agent.speed = stats.walkSpeed;
        agent.Warp(spawnPos);
        agent.destination = GenerateRandomNavMeshPos();
    }

    public virtual void PreInitialize()
    {
        stateMachine = new AnimalStateMachine(this);
        stateMachine.InitStateMachine();

        ragdoll.PreInitialize();
    }

    public virtual void Initialize()
    {
        SetDelgsForStateMachine();
        ragdoll.Initialize();

        offset = transform.up;
    }

    public virtual void Refresh()
    {
        stateMachine.UpdateStateMachine();
        UpdateRotation();
        UpdateAnimationSpeedVariable();
        ragdoll.Refresh();

        if (chaseTarget == null)
        {
            FindVisibleTargets(stats.targetTagName);
        }

        AdaptToSlopeAngle();
    }

    public virtual void PhysicsRefresh()
    {
        ragdoll.PhysicsRefresh();
    }

    public Vector3 GenerateRandomNavMeshPos(int areaMask = NavMesh.AllAreas)
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomDirection = Random.insideUnitSphere.normalized * stats.walkRadius;
        randomDirection += transform.position;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, stats.walkRadius + transform.position.y, areaMask))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public Vector3 GetNavMeshPosition(Vector3 direction, float lenght, int areaMask = NavMesh.AllAreas)
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 targetDirection = direction * lenght;
        targetDirection += transform.position;
        if (NavMesh.SamplePosition(targetDirection, out NavMeshHit hit, lenght + transform.position.y, areaMask))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public bool CheckIfDestinationReached()
    {
        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    public void UpdateRotation()
    {
        if (agent.velocity.sqrMagnitude >= 0.1f)
        {
            Vector3 moveDirection = agent.destination - transform.position;
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * stats.rotationSpeed);
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void FindVisibleTargets(string targetTagName)
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, stats.viewRadius, stats.targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            if (target.CompareTag(targetTagName))
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < stats.viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, stats.obstacleMask))
                    {
                        chaseTarget = target.gameObject;
                    }
                }
            }
        }
    }

    public void UpdateAnimationSpeedVariable()
    {
        float currentSpeed = agent.velocity.magnitude;
        float maxSpeed = agent.speed;
        float speedRatio = currentSpeed / maxSpeed;

        float animSpeed = 0f;

        if (agent.speed == stats.walkSpeed)
            animSpeed = Mathf.Clamp01(speedRatio) * 0.7f;
        else if (agent.speed == stats.runSpeed)
            animSpeed = Mathf.Clamp01(speedRatio);

        animController.SetFloat("Speed", animSpeed);
    }

    public Vector3 GetAwayFromPlayersDirection()
    {
        var Players = GameObject.FindGameObjectsWithTag("Player");

        Vector3 averagePosition = (Players[0].transform.position + Players[1].transform.position) / 2;
        Vector3 directionToAverage = averagePosition - transform.position;
        Vector3 awayDirection = -directionToAverage.normalized;

        return awayDirection;
    }



    public void AdaptToSlopeAngle()
    {
        Vector3 origin = transform.position;

        int hillLayerIndex = LayerMask.NameToLayer("Ground");
        int layerMask = (1 << hillLayerIndex);

        RaycastHit slopeHit;

        if (Physics.Raycast(origin + offset, Vector3.down, out slopeHit, maxRayDist, layerMask))
        {
            Debug.DrawLine(origin, slopeHit.point, Color.red);

            Quaternion newRot = Quaternion.FromToRotation(transform.up, slopeHit.normal) * transform.rotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * slopeRotChangeSpeed);
        }
    }

    #region StateMachine Setup
    public void SetDelgsForStateMachine()
    {
        //OnEnter
        stateMachine.OnPatrolEnter += () => { OnPatrolEnter(); };
        stateMachine.OnFleeEnter += () => { OnFleeEnter(); };
        stateMachine.OnChaseEnter += () => { OnChaseEnter(); };
        stateMachine.OnSpecialActionEnter += () => { OnSpecialActionEnter(); };
        //OnLogic
        stateMachine.OnPatrolLogic += () => { OnPatrolLogic(); };
        stateMachine.OnFleeLogic += () => { OnFleeLogic(); };
        stateMachine.OnChaseLogic += () => { OnChaseLogic(); };
        stateMachine.OnSpecialActionLogic += () => { OnSpecialActionLogic(); };

        //OnExit
        stateMachine.OnPatrolExit += () => { OnPatrolExit(); };
        stateMachine.OnFleeExit += () => { OnFleeExit(); };
        stateMachine.OnChaseExit += () => { OnChaseExit(); };
        stateMachine.OnSpecialActionExit += () => { OnSpecialActionExit(); };

        //Transitions
        stateMachine.IsPatrolFinished = () => { return IsPatrolFinished(); };
        stateMachine.IsFleeFinished = () => { return IsFleeFinished(); };
        stateMachine.IsFleeTime = () => { return IsFleeTime(); };
        stateMachine.IsChaseFinished = () => { return IsChaseFinished(); };
        stateMachine.IsSpecialActionFinished = () => { return IsSpecialActionFinished(); };
        stateMachine.IsSpecialActionTime = () => { return IsSpecialActionTime(); };
    }

    #region OnEnter
    public virtual void OnPatrolEnter()
    {
        agent.speed = stats.walkSpeed;
        agent.destination = GenerateRandomNavMeshPos();
    }

    public virtual void OnFleeEnter()
    {
        chaseTarget = null;
        agent.speed = stats.runSpeed;

        agent.destination = GetNavMeshPosition(GetAwayFromPlayersDirection(), stats.fleeDistance);
    }

    public virtual void OnChaseEnter()
    {
        agent.speed = stats.runSpeed;
    }

    public virtual void OnSpecialActionEnter()
    {

    }
    #endregion

    #region OnLogic
    public virtual void OnPatrolLogic()
    {
        stateMachine.CurrentState = AnimalStateMachine.Patrol;
        if (CheckIfDestinationReached())
        {
            agent.destination = GenerateRandomNavMeshPos();
        }
    }

    public virtual void OnFleeLogic()
    {
        stateMachine.CurrentState = AnimalStateMachine.Flee;

        if (CheckIfDestinationReached())
        {
            isScared = false;
        }
    }

    public virtual void OnChaseLogic()
    {
        stateMachine.CurrentState = AnimalStateMachine.Chase;
        if (chaseTarget != null)
            agent.destination = chaseTarget.transform.position;
    }

    public virtual void OnSpecialActionLogic()
    {
        stateMachine.CurrentState = AnimalStateMachine.SpecialAction;
    }

    #endregion

    #region OnExit
    public virtual void OnPatrolExit()
    {

    }

    public virtual void OnFleeExit()
    {

    }

    public virtual void OnChaseExit()
    {
        agent.speed = stats.walkSpeed;
    }

    public virtual void OnSpecialActionExit()
    {

    }
    #endregion

    #region Transitons
    public virtual bool IsPatrolFinished()
    {
        if (chaseTarget != null)
            return true;
        else
            return false;
    }

    public virtual bool IsFleeFinished()
    {
        if (!isScared)
            return true;
        else
            return false;
    }

    public virtual bool IsFleeTime()
    {
        if (isScared)
            return true;
        else
            return false;
    }

    public virtual bool IsChaseFinished()
    {
        if (chaseTarget == null)
            return true;
        else
            return false;
    }

    public virtual bool IsSpecialActionFinished()
    {
        return true;
    }

    public virtual bool IsSpecialActionTime()
    {
            return false;
    }
    #endregion

    #endregion
}
