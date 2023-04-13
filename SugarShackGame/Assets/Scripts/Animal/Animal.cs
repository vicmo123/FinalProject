using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour, IFlow
{
    public AnimalStats stats;
    public NavMeshAgent agent;
    
    public virtual void PreInitialize()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.Warp(Vector3.zero);
        agent.speed = stats.walkSpeed;
        agent.destination = GenerateRandomNavMeshPos();
        Debug.Log("allo");
    }

    public virtual void Initialize()
    {

    }

    public virtual void Refresh()
    {
        if (CheckIfDestinationReached())
        {
            agent.destination = GenerateRandomNavMeshPos();
        }
    }

    public virtual void PhysicsRefresh()
    {

    }

    public Vector3 GenerateRandomNavMeshPos()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized * stats.walkRadius;
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
}
