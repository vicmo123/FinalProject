using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ParticleEffectPool
{
    private Dictionary<ParticleEffectType, Queue<GameObject>> particleEffectPool;

    public ParticleEffectPool()
    {
        Initialize();
    }

    public void Initialize()
    {
        particleEffectPool = new Dictionary<ParticleEffectType, Queue<GameObject>>();

        List<ParticleEffectType> enums = System.Enum.GetValues(typeof(ParticleEffectType)).Cast<ParticleEffectType>().ToList();

        for (int i = 0; i < enums.Count; i++)
        {
            particleEffectPool[enums[i]] = new Queue<GameObject>();
        }
    }

    public void Pool(ParticleEffectType particleEffectType, GameObject objToPool)
    {
        objToPool.SetActive(false);
        particleEffectPool[particleEffectType].Enqueue(objToPool);
    }

    public GameObject Depool(ParticleEffectType type)
    {
        GameObject toRet = (particleEffectPool[type].Count > 0) ? particleEffectPool[type].Dequeue() : null;
        if (toRet)
            toRet.SetActive(true);

        return toRet;
    }
}
