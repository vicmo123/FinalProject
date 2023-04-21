using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : IFlow
{
    private Ability ability;
    private bool isEmpty;

    public void PreInitialize()
    {
        FillSlot();
    }

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }

    public void Refresh()
    {
        if (isEmpty == false)
        {
            if (ability.state == AbilityState.Done)
            {
                EmptySlot();
            }
        }
    }


    public void FillSlot()
    {
        isEmpty = false;
        string abilityName = AbilityManager.Instance.RandomAbility();
        //TODO
        //Verify if it can use this ability
        ability = AbilityManager.Instance.CreateAbility(abilityName);
        Debug.Log("Slot : Ability available = " + ability.name);
    }

    public void EmptySlot()
    {
        isEmpty = true;
        ability = null;
        Debug.Log("Slot is empty.");
        //TODO
        //wait 3 seconds 
        //then call a generating new ability
    }
    public Ability GetAbility()
    {
        return ability;
    }
}
