using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class AbilityFactory
{
    //Dictionary<string, AbilityHolder> abilityDict;
    //List<string> abilityNames;

    //public AbilityFactory()
    //{
    //    InitializeDictResources();
    //}

    //public AbilityHolder CreateAbility(string abilityName)
    //{
    //    AbilityHolder abHolder;

    //    if (abilityDict.ContainsKey(abilityName))
    //        abHolder = GameObject.Instantiate<AbilityHolder>(abilityDict[abilityName]);
    //    else
    //        throw new System.Exception("Ability Factory : The name of the ability doesn't correspond to an existing ability in the resources dictionnary");

    //    return abHolder;
    //}

    //public string GetRandomAbilityName()
    //{        
    //    return abilityNames[Random.Range(0, abilityNames.Count)];
    //}

    //private void InitializeDictResources()
    //{
    //    //populate the dictionnary with AbilityHolder Prefabs
    //    abilityDict = new Dictionary<string, AbilityHolder>();
    //    string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Scripts/Abilities/AbilityStats" });

    //    if (prefabGuids.Length == 0)
    //        throw new System.Exception("AbilityFactory :  no AbilityHolder prefab was found in the file AbilityStats. Have they been moved to another file ?");

    //    foreach (string guid in prefabGuids)
    //    {
    //        string path = AssetDatabase.GUIDToAssetPath(guid);
    //        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    //        string prefabName = prefab.name;
    //        AbilityHolder ab = prefab.GetComponent<AbilityHolder>();
    //        abilityDict.Add(prefabName, ab);
    //    }

    //    //Populate dictionnary of names (list of strings)
    //    InitializeNameDict();
    //}

    //private void InitializeNameDict()
    //{
    //    abilityNames = new List<string>();

    //    foreach (KeyValuePair<string, AbilityHolder> el in abilityDict)
    //    {
    //        abilityNames.Add(el.Key);
    //    }
    //}
}