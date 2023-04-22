using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    private string folderPath = "Prefabs/Abilities/";
    private Dictionary<string, Ability> abilityDict;
    public string[] abilityNames;

    public AbilityFactory()
    {
        InitializeDictResources();
    }

    private void InitializeDictResources()
    {
        //populate the dictionnary with AbilityHolder Prefabs
        abilityDict = new Dictionary<string, Ability>();
        GameObject[] prefabs = Resources.LoadAll<GameObject>(folderPath);

        abilityNames = new string[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            abilityDict.Add(prefabs[i].name, prefabs[i].GetComponent<Ability>());
            abilityNames[i] = prefabs[i].name;
        }        
    }

    public Ability CreateAbility(string abilityName)
    {
        Ability newAbility = GameObject.Instantiate<Ability>(abilityDict[abilityName]);

        newAbility.gameObject.SetActive(false);

        if (newAbility != null)
            return newAbility;
        else
            throw new System.Exception("Ability Factory : The name of the ability doesn't correspond to an existing ability in the resources dictionnary");
                
    }
}
