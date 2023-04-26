using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour, IFlow
{
    [HideInInspector]
    public Ability abilityStats;
    [HideInInspector]
    public bool readyToBeDestroyed = false;
    [HideInInspector]
    public CountDownTimer timer;
    [HideInInspector]
    public Player player = null;

    private bool isActive = true;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void Deactivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }

    public virtual void Initialize()
    {
        readyToBeDestroyed = false;
        timer = new CountDownTimer(abilityStats.timeBeforeDestruction, false);
        timer.OnTimeIsUpLogic = () => { readyToBeDestroyed = true; };
    }

    public virtual void PhysicsRefresh()
    {
    }

    public virtual void PreInitialize()
    {
    }

    public virtual void Refresh()
    {
        timer.UpdateTimer();
    }

    public virtual void InitAbility(Ability _stats, Player _player)
    {
        abilityStats = _stats;
        player = _player;

        Initialize();
        PreInitialize();
    }
}
