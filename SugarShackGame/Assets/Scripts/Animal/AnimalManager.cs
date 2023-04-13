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

    private enum AnimalTypes
    {
        BEAR,
        DEER,
        FOX
    }

    private List<Animal> animalList;
    private int maxNumberAnimals = 10;
    private int intialNumberOfAnimals = 3;
    private Vector2 rangeTimeOfSpawn = new Vector2(3, 5);

    public void PreInitialize()
    {
        animalList = new List<Animal>();
        Debug.Log("Animals");

        foreach (Animal animal in animalList)
        {
            animal.PreInitialize();
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < intialNumberOfAnimals; i++)
        {
            animalList.Add(GetRandomAnimal());
        }

        foreach (Animal animal in animalList)
        {
            animal.Initialize();
        }
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

    private Animal GetRandomAnimal()
    {
        int AnimalTypesMemberCount = System.Enum.GetNames(typeof(AnimalTypes)).Length;
        AnimalTypes chosenAnimalType = (AnimalTypes)Random.Range(0, AnimalTypesMemberCount - 1);
        Animal toRet = new Animal();

        switch (chosenAnimalType)
        {
            case AnimalTypes.BEAR:
                toRet = new Bear();
                break;
            case AnimalTypes.DEER:
                toRet = new Deer();
                break;
            case AnimalTypes.FOX:
                toRet = new Fox();
                break;
            default:
                break;
        }

        return toRet;
    }
}
