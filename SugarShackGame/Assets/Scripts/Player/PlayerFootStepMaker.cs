using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStepMaker : MonoBehaviour, IFlow
{ 
    public GameObject footStepPrefab;
    public Transform[] feetObjects;
    public LayerMask groundMask;
    public float footprintOffset = 0.05f;
    public float maxFootprintDistance = 0.1f;
    public int prefillAmount = 10;
    public int framesPerFootprint = 10;

    private PlayerAnimationEvents animEvents;

    private footsteps.FootprintFactoryPool factoryPool;
    private List<FootPrint> inGamePrintsList = new List<FootPrint>();

    private float XRotationVal = 90;
    private int currentFrame = 0;

    private Transform footStepContainer;
    public void PreInitialize()
    {
        factoryPool = new footsteps.FootprintFactoryPool(footStepPrefab);
    }

    public void Initialize()
    {
        animEvents = GetComponent<PlayerAnimationEvents>();
        animEvents.OnRightStep += () =>
        {
            GenerateFootSteps(feetObjects[1]);
        };
        animEvents.OnLeftStep += () =>
        {
            GenerateFootSteps(feetObjects[0]);
        };

        footStepContainer = GameObject.FindGameObjectWithTag("FootStepContainer").transform;
    }

    public void PhysicsRefresh()
    {
        FixedUpdateFootPrints();
    }

    public void Refresh()
    {
        UpdateFootPrints();
    }

    private void UpdateFootPrints()
    {
        foreach (var item in inGamePrintsList)
        {
            if (item != null)
                item.Refresh();
        }

        if (inGamePrintsList.Count > 0)
        {
            for (int i = inGamePrintsList.Count - 1; i >= 0; i--)
            {
                if (inGamePrintsList[i].isReadyToBeDestoryed)
                {
                    factoryPool.Pool(inGamePrintsList[i]);
                    inGamePrintsList.RemoveAt(i);
                }
            }
        }
    }

    private void FixedUpdateFootPrints()
    {
        foreach (var item in inGamePrintsList)
        {
            if (item != null)
                item.PhysicsRefresh();
        }
    }

    private void GenerateFootSteps(Transform foot)
    {
        RaycastHit hit;
        if (Physics.Raycast(foot.position, Vector3.down, out hit, maxFootprintDistance, groundMask))
        {
            FootPrint newFootprint = factoryPool.Create(hit.point + hit.normal * footprintOffset, Quaternion.identity);

            Vector3 newFootprintUp = hit.normal;
            Vector3 newFootprintForward = Vector3.Cross(foot.right, newFootprintUp);
            newFootprint.transform.rotation = Quaternion.LookRotation(newFootprintForward, newFootprintUp);

            inGamePrintsList.Add(newFootprint);
            newFootprint.timer.OnTimeIsUpLogic = () =>
            {
                newFootprint.isReadyToBeDestoryed = true;
            };

            newFootprint.isReadyToBeDestoryed = false;
            newFootprint.timer.StartTimer();

            if (footStepContainer)
            {
                newFootprint.transform.SetParent(footStepContainer);
            }
        }
    }
}
