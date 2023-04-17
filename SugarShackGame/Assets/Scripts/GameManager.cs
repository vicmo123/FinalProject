using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IFlow
{
    #region Singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    private GameManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    List<IFlow> managerList;

    public void GameManagerSetup()
    {
        managerList = new List<IFlow>();

        //Add your system manager here : 
        FillManagerList(
            AnimalManager.Instance, 
            AbilityManager.Instance,
            PlayerManager.Instance
            );
    }

    public void PreInitialize()
    {
        foreach (var manager in managerList)
        {
            manager.PreInitialize();
        }
    }

    public void Initialize()
    {
        foreach (var manager in managerList)
        {
            manager.Initialize();
        }
    }

    public void Refresh()
    {
        foreach (var manager in managerList)
        {
            manager.Refresh();
        }
    }

    public void PhysicsRefresh()
    {
        foreach (var manager in managerList)
        {
            manager.PhysicsRefresh();
        }
    }

    private void FillManagerList(params IFlow[] managers)
    {
        foreach (var manager in managers)
        {
            managerList.Add(manager);
        }
    }
}
