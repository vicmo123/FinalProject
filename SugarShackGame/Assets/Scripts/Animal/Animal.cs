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

    public virtual void PreInitialize()
    { 
        stateMachine = new AnimalStateMachine(this);
        stateMachine.InitStateMachine();

        agent.speed = stats.walkSpeed;
        agent.Warp(Vector3.zero);
        agent.destination = GenerateRandomNavMeshPos();

        ragdoll.PreInitialize();
    }

    public virtual void Initialize()
    {
        SetDelgsForStateMachine();
        ragdoll.Initialize();
    }

    public virtual void Refresh()
    {
        stateMachine.UpdateStateMachine();
        UpdateRotation();
        UpdateAnimationSpeedVariable();
        ragdoll.Refresh();

        if(chaseTarget == null)
        {
            FindVisibleTargets(stats.targetTagName);
        }
    }

    public virtual void PhysicsRefresh()
    {
        ragdoll.PhysicsRefresh();
    }

    public Vector3 GenerateRandomNavMeshPos()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomDirection = Random.insideUnitSphere.normalized * stats.walkRadius;
        randomDirection += transform.position;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, stats.walkRadius + transform.position.y, NavMesh.AllAreas))
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
        Debug.Log("Time to flee");
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
        if(chaseTarget == null)
            return true;
        else
            return false;
    }

    public virtual bool IsSpecialActionFinished()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return true;
        else
            return false;
    }

    public virtual bool IsSpecialActionTime()
    {
        if (Input.GetKeyDown(KeyCode.T))
            return true;
        else
            return false;
    }
    #endregion

    #endregion
}
