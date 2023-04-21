using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Manager(typeof(AbilityManager))]
public class AbilityManager : IFlow
{
    private AbilityFactory abilityFactory;
    private List<Ability> abilityList;
    private List<Ability> toRemove;
    

    #region Singleton
    private static AbilityManager instance;

    public static AbilityManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AbilityManager();
            }
            return instance;
        }
    }

    private AbilityManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    public void PreInitialize()
    {
        Debug.Log("AbilityManager");
        abilityFactory = new AbilityFactory();
        abilityList = new List<Ability>();
        toRemove = new List<Ability>();
    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
        //Manage list of abilities that no longer need to be refreshed (need to be destroyed)
        if (toRemove.Count > 0)
        {
            for (int i = (toRemove.Count - 1); i >= 0; i--)
            {
                abilityList.Remove(toRemove[i]);
                GameObject.Destroy(toRemove[i]);
            }
            toRemove.Clear();
        }

        foreach(Ability ability in abilityList)
        {
            if (ability.state != AbilityState.Done)
            {
                ability.Refresh();
            }
            else
            {

                toRemove.Add(ability);
            }
        }
    }

    public void PhysicsRefresh()
    {
        foreach (Ability ability in abilityList)
        {
            ability.PhysicsRefresh();
        }
    }

    public Ability CreateAbility(string abilityName = null)
    {
        if (abilityName == null)
        {
            abilityName = RandomAbility();
        }

        Ability newAbility = abilityFactory.CreateAbility(abilityName);

        if (newAbility != null)
        {
            newAbility.PreInitialize();
            newAbility.Initialize();

            abilityList.Add(newAbility);
        }

        return newAbility;
    }

    public string RandomAbility()
    {
        int randomNameIndex = Random.Range(0, abilityFactory.abilityNames.Length);
        return abilityFactory.abilityNames[randomNameIndex];
    }
}