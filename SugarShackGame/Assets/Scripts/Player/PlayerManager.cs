using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IFlow
{
    #region Singleton
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerManager();
            }
            return instance;
        }
    }

    private PlayerManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    public void PreInitialize()
    {

    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
    }

    public void PhysicsRefresh()
    {
    }
}
