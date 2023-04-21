using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState { Ready, Active, Cooldown, Done };
public class Ability : MonoBehaviour, IFlow
{
    public AbilityStats stats;
    public AbilityState state;
    private float activeTimeLeft;
    private float cooldownTimeLeft;


    public virtual void PreInitialize()
    {
        Debug.Log("Base Ability Initialize");
        this.activeTimeLeft = stats.activeTime;
        this.cooldownTimeLeft = stats.cooldownTime;
        this.state = AbilityState.Ready;
    }

    public virtual void Initialize()
    {

    }

    public virtual void PhysicsRefresh()
    {
    }


    public virtual void Refresh()
    {
        switch (state)
        {
            case AbilityState.Ready:
                break;
            case AbilityState.Active:
                if (activeTimeLeft <= 0)
                {
                    state = AbilityState.Cooldown;
                }
                else
                {
                    activeTimeLeft -= Time.deltaTime;
                }
                break;
            case AbilityState.Cooldown:
                if (cooldownTimeLeft <= 0)
                {
                    state = AbilityState.Done;
                }
                else
                {
                    cooldownTimeLeft -= Time.deltaTime;
                }
                break;
            case AbilityState.Done:
                break;
            default:
                break;
        }
    }

    public virtual void Activate(Player player)
    {
        this.state = AbilityState.Active;
        this.gameObject.SetActive(true);
        if(stats.isThowable == true)
        {
            //TODO
            //ask property of the player : snowballStartPosition
            transform.position = player.transform.position;
        }
        else
        {
            //TODO
            //ask property of the player : abilityStartPosition
            transform.position = player.transform.position;
        }
    }
}