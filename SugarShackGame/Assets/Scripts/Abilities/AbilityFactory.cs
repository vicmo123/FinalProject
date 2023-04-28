using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class AbilityFactory
{
    private Dictionary<AbilityType, Ability> resourceDict;
    private List<AbilityType> abilityNames;

    public AbilityFactory()
    {
        resourceDict = new Dictionary<AbilityType, Ability>();
        LoadResources();
    }

    public void LoadResources()
    {
        abilityNames = System.Enum.GetValues(typeof(AbilityType)).Cast<AbilityType>().ToList();

        foreach (var name in abilityNames)
        {
            var abilityObj = (Ability)ScriptableObject.CreateInstance(name + "Ability");
            resourceDict.Add(name, abilityObj);

            System.Type t = Assembly.GetExecutingAssembly().GetType(name.ToString());
            if (t.IsSubclassOf(typeof(ThrowableAbility))) {
                resourceDict[name].isThrowable = true;
            }
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
