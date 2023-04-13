using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : IFlow
{
    #region Singleton
    private static AnimalManager instance;

    public static AnimalManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AnimalManager();
            }
            return instance;
        }
    }

    private AnimalManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    private List<Animal> animalList;

    void IFlow.PreInitialize()
    {
        animalList = new List<Animal>();
    }

    void IFlow.Initialize()
    {
        throw new System.NotImplementedException();
    }

    void IFlow.Refresh()
    {
        throw new System.NotImplementedException();
    }

    void IFlow.PhysicsRefresh()
    {
        throw new System.NotImplementedException();
    }
}
