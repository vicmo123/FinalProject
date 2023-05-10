using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : Animal
{
    private CountDownTimer standUpTimer;
    private Vector2 standUpTimeRange = new Vector2(15, 25);

    private bool isStandingUpTime = false;
    private bool animationIsOver = false;

    public override void Initialize()
    {
        base.Initialize();
        standUpTimer = new CountDownTimer(GetRadomTime(), false);
        standUpTimer.OnTimeIsUpLogic = () => { 
            standUpTimer.SetDuration(GetRadomTime());
            isStandingUpTime = true;
            animationIsOver = false;
            standUpTimer.StartTimer();
        };

        standUpTimer.StartTimer();
    }

    public override void Refresh()
    {
        base.Refresh();
        standUpTimer.UpdateTimer();
    }

    public override void OnSpecialActionEnter()
    {
        isStandingUpTime = false;

        base.OnSpecialActionEnter();
        animController.SetTrigger("StandUp");
        var agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    public override bool IsSpecialActionTime()
    {
        return isStandingUpTime;
    }

    public override bool IsSpecialActionFinished()
    {
        return animationIsOver;
    }

    private float GetRadomTime()
    {
        return Random.Range(standUpTimeRange.x, standUpTimeRange.y);
    }

    public void AnimIsOverEvent()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        animationIsOver = true;
    }
}
