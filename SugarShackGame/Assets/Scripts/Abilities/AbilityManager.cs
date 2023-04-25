using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Manager(typeof(AbilityManager))]
public class AbilityManager/* IFlow*/
{
    //#region Singleton
    //private static AbilityManager instance;

    //public static AbilityManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new AbilityManager();
    //        }
    //        return instance;
    //    }
    //}

    //private AbilityManager()
    //{
    //}
    //#endregion
    //AbilityFactory factory;
    //List<AbilityHolder> inUse;
    //List<AbilityHolder> inCooldown;
    //string defaultAbility = "Apple";


    //public void PreInitialize()
    //{
    //    factory = new AbilityFactory();
    //    inUse = new List<AbilityHolder>();
    //    inCooldown = new List<AbilityHolder>();
    //}

    //public void Initialize()
    //{
    //}

    //public void PhysicsRefresh()
    //{
    //}


    //public void Refresh()
    //{
    //    for (int i = inUse.Count - 1; i >= 0; i--)
    //    {
    //        //if ability is in cooldown, stop updating.
    //        if (inUse[i].GetState() == AbilityState.Cooldown)
    //        {                
    //            inCooldown.Add(inUse[i]);
    //            inUse.Remove(inUse[i]);
    //        }
    //        else
    //        {
    //            inUse[i].Refresh();
    //        }
    //    }

    //    for (int i = inCooldown.Count - 1; i >= 0; i--)
    //    {
    //        //refresh the cooldown list : running their countdown timer 
    //        //when timer is at 0, remove from list
    //    }
    //}

    //public AbilityHolder GenerateAbility()
    //{
    //    int tries = 0;
    //    string name = defaultAbility;

    //    while (tries < 30)
    //    {
    //        //Create Ability by asking the factory
    //        name = factory.GetRandomAbilityName();
            
    //        bool isOnCooldown = false;

    //        //Verify this ability is not part of the cooldown list
    //        foreach (AbilityHolder ability in inCooldown)
    //        {
    //            if (ability.name == name)
    //            {
    //                isOnCooldown = true;
    //                break;
    //            }
    //        }
    //        if (!isOnCooldown)
    //            break;

    //        tries++;
    //    }

    //    //default ability
    //    if (tries == 30)
    //    {
    //        name = defaultAbility;
    //    }
    //    AbilityHolder newAbility = factory.CreateAbility(name);

    //    //Initialize the ability
    //    newAbility.PreInitialize();
    //    inUse.Add(newAbility);

    //    return newAbility;
    //}
}