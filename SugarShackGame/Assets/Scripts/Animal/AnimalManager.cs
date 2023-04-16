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

    private AnimalFactory animalFactory;
    private List<Animal> animalList;
    private int maxNumberAnimals = 10;
    private int initialNumberOfAnimals = 25;
    private Vector2 rangeTimeOfSpawn = new Vector2(3, 5);

    public void PreInitialize()
    {
        animalFactory = new AnimalFactory();
        animalList = new List<Animal>();
    }

    public void Initialize()
    {
        populateMap();
    }

    public void Refresh()
    {
        foreach (Animal animal in animalList)
        {
            animal.Refresh();
        }
    }

    public void PhysicsRefresh()
    {
        foreach (Animal animal in animalList)
        {
            animal.PhysicsRefresh();
        }
    }

    public void populateMap()
    {
        AddAnimal("Deer");
        for (int i = 0; i < initialNumberOfAnimals; i++)
        {
            AddAnimal();
        }
    }

    private void AddAnimal(string animalName = null)
    {
        if (animalName == null)
        {
            int randomNameIndex = Random.Range(0, animalFactory.animalNames.Length);
            animalName = animalFactory.animalNames[randomNameIndex];
        }

        Animal newAnimal = animalFactory.CreateAnimal(animalName);
        
        if (newAnimal != null)
        {
            newAnimal.PreInitialize();
            newAnimal.Initialize();

            animalList.Add(newAnimal);
            newAnimal.transform.position = Vector3.zero;
        }
    }
}
