using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : IFlow
{
    #region singleton
    private static AbilityManager instance;
    private AbilityManager() { }

    public static AbilityManager Instance()
    {
        if (instance == null)
        {
            instance = new AbilityManager();
        }
        return instance;
    }
    #endregion

    public void PreInitialize()
    {
        Debug.Log("Ability Manager PreInit");
    }

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    } 


    public void Refresh()
    {
    }
}
