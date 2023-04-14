using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFactory
{
    private string folderPath = "Prefabs/Animals/";
    private Dictionary<string, Animal> animalPrefabMap;
    public string[] animalNames;

    public AnimalFactory()
    {
        animalPrefabMap = new Dictionary<string, Animal>();
        FillAnimalPrefabMap();
    }

    private void FillAnimalPrefabMap()
    {
        var prefabs = Resources.LoadAll<GameObject>(folderPath);
        animalNames = new string[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            animalPrefabMap.Add(prefabs[i].name, prefabs[i].GetComponent<Animal>());
            animalNames[i] = prefabs[i].name;
        }
    }

    public Animal CreateAnimal(string animalName)
    {
        var newAnimal = GameObject.Instantiate(animalPrefabMap[animalName]);

        if (newAnimal != null)
        {
            return newAnimal;
        }
        else
        {
            Debug.Log("Unable to create desired animal of name: " + animalName);
            return null;
        }
    }
}
