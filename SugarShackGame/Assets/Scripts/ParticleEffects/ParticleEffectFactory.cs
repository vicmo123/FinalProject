using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum ParticleEffectType { Fire, Fireworks, Spilling }
public class ParticleEffectFactory
{
    private ParticleEffectPool pool;
    Dictionary<ParticleEffectType, GameObject> resourceDict;
    List<ParticleEffectType> partEffectTypes;

    private const string folderPath = "Prefabs/Particle_Effects";

    public ParticleEffectFactory()
    {
        pool = new ParticleEffectPool();
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
        GameObject toRet = ReleaseObject(type);
        if (toRet == null)
        {
            toRet = GameObject.Instantiate<GameObject>(resourceDict[type]);
        }
        return toRet;
    }

    private GameObject ReleaseObject(ParticleEffectType particleEffectType)
    {
        GameObject toRet = pool.Depool(particleEffectType);
        if (toRet == null)
        {
            return null;
        }
        return toRet;
    }

    public void GiveToPool(GameObject toPool)
    {
        foreach (var item in partEffectTypes)
        {
            if (item.ToString().Equals(toPool.name))
            {
                pool.Pool(item, toPool);
            }
        }       
    }
}
