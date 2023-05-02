using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Manager(typeof(AnimalManager))]
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
    private int maxNumberAnimals = 25;
    private Vector2 rangeTimeOfSpawn = new Vector2(3.0f, 5.0f);
    private CountDownTimer timer;
    private Transform[] spawnPoints;

    private void IntializeSpawnPoints()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("AnimalSpawn");
        spawnPoints = new Transform[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            spawnPoints[i] = gameObjects[i].transform;
        }
    }

    public void PreInitialize()
    {
        animalFactory = new AnimalFactory();
        animalList = new List<Animal>();

        timer = new CountDownTimer(Random.Range(rangeTimeOfSpawn.x, rangeTimeOfSpawn.y), true);
        timer.OnTimeIsUpLogic += () => {
            if (animalList.Count < maxNumberAnimals)
                SpawnAnimal();
            timer.SetDuration(Random.Range(rangeTimeOfSpawn.x, rangeTimeOfSpawn.y));
        };
    }

    public void Initialize()
    {
        IntializeSpawnPoints();
        timer.StartTimer();
    }

    public void Refresh()
    {
        timer.UpdateTimer();

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

    private void SpawnAnimal(string animalName = null)
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

            int spawnIndex = Random.Range(0, spawnPoints.Length);
            newAnimal.SpawnAtPosition(new Vector3(spawnPoints[spawnIndex].position.x, 0, spawnPoints[spawnIndex].position.z));
        }
    }
}
