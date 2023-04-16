using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum AbilityState { Ready, Active, Cooldown }

public class AbilityHolder : MonoBehaviour, IFlow
{
    public Ability ability;
    private AbilityState state;
    private bool isActive;
    private float activeTime;
    private float cooldownTime;

    public bool IsActive { get => isActive; set => isActive = value; }

    public void PreInitialize()
    {
        state = AbilityState.Ready;
        activeTime = ability.activeTime;
        cooldownTime = ability.cooldownTime;
        IsActive = false;
    }

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }

    public void Refresh()
    {
        if(this.isActive == true)        
            this.state = AbilityState.Active;
        

        switch (state)
        {
            case AbilityState.Ready:
                activeTime = ability.activeTime;

                if (this.isActive == true)                
                    this.state = AbilityState.Active;                

                break;
            case AbilityState.Active:
                if (ability.activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.Cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.Cooldown:
                if (ability.cooldownTime > 0)
                {
                    this.isActive = false;
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.Ready;
                }
                break;
            default:
                break;
        }
    }
    public AbilityState GetState()
    {
        return this.state;
    }

    public Ability GetAbilityStats()
    {
        return this.ability;
    }
}