using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum ParticleEffectType { Fire, Fireworks, Spilling, FloatingText, SnowSteps, Dust, Trail }
public class ParticleEffectFactory
{
    Dictionary<ParticleEffectType, GameObject> resourceDict;
    List<ParticleEffectType> partEffectTypes;

    private const string folderPath = "Prefabs/Particle_Effects";

    public ParticleEffectFactory()
    {
        resourceDict = new Dictionary<ParticleEffectType, GameObject>();
        LoadResources();
    }

    private void LoadResources()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>(folderPath);

        partEffectTypes = Enum.GetValues(typeof(ParticleEffectType)).Cast<ParticleEffectType>().ToList();

        for (int i = 0; i < partEffectTypes.Count; i++)
        {
            for (int j = 0; j < prefabs.Length; j++)
            {
                if (prefabs[j].name.Equals(partEffectTypes[i].ToString()))
                {
                    resourceDict.Add(partEffectTypes[i], prefabs[j]);
                    continue;
                }
            }
        }

    }

    public GameObject CreateParticleEffect(ParticleEffectType type)
    {
        return GameObject.Instantiate<GameObject>(resourceDict[type]);
    }


}
