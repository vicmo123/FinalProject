using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : IFlow
{
    #region singleton
    private static AbilityManager instance;
    private AbilityManager() { }

    public static AbilityManager GetInstance()
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
