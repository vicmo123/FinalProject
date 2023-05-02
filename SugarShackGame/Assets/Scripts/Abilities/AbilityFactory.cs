using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory
{
    private Dictionary<AbilityType, Ability> resourceDict;
    private List<AbilityType> abilityNames;

    private const string folderPath = "AbilitiesScriptable/";

    public AbilityFactory()
    {
        resourceDict = new Dictionary<AbilityType, Ability>();
        abilityNames = new List<AbilityType>();
        LoadResources();
    }

    public void LoadResources()
    {
        Object[] assets = Resources.LoadAll(folderPath, typeof(Ability));
        foreach (Ability scriptable in assets)
        {
            resourceDict.Add(scriptable.type, scriptable);
            abilityNames.Add(scriptable.type);
        }

        //For the random to not create snowballs
        abilityNames.Remove(AbilityType.SnowBall);
    }

    public Ability CreateAbility(AbilityType type)
    {
        return resourceDict[type];
    }

    public Ability CreateRandomAbility()
    {
        return resourceDict[abilityNames[Random.Range(0, abilityNames.Count)]];
    }
}
