using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : IFlow
{
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
