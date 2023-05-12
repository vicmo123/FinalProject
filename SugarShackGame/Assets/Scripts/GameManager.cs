using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

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

        //Must add ManagerAttribute to your Manager for it to be added to the list. It must be a singleton with Instance property
        FillManagerList();
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
           // Debug.Log("GameManager Resfresh");
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
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var attributes = type.GetCustomAttributes(typeof(ManagerAttribute), true);
            if (attributes.Length > 0)
            {
                ManagerAttribute managerAttribute = (ManagerAttribute)attributes[0];
                if (typeof(IFlow).IsAssignableFrom(managerAttribute.ManagerType))
                {
                    var instanceProperty = managerAttribute.ManagerType.GetProperty("Instance");
                    var instance = instanceProperty.GetValue(null);
                    if (instance != null && instance is IFlow)
                    {
                        AddManagerToList((IFlow)instance);
                    }
                }
            }
        }
    }

    private void AddManagerToList(IFlow toAdd)
    {
        managerList.Add(toAdd);
    }
}
