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

    public void PreInitialize()
    {
        animalList = new List<Animal>();
        Debug.Log("Animals");
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
