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

    Player player1;
    Player player2;

    public void PreInitialize()
    {
        player1.PreInitialize();
        player2.PreInitialize();
    }

    public void Initialize()
    {
        player1.Initialize();
        player2.Initialize();
    }

    public void Refresh()
    {
        player1.Refresh();
        player2.Refresh();
    }

    public void PhysicsRefresh()
    {
        player1.PhysicsRefresh();
        player2.PhysicsRefresh();
    }
}
