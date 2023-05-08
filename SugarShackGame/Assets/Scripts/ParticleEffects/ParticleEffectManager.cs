using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Manager(typeof(PlayerManager))]
public class ParticleEffectManager : IFlow
{
    #region Singleton
    private static ParticleEffectManager instance;
    private bool paused = false;

    public static ParticleEffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ParticleEffectManager();
            }
            return instance;
        }
    }

    private ParticleEffectManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    private List<ParticleEffectReusable> particleEffects;
    private ParticleEffectFactory factory;

    public GameObject Create(ParticleEffectType type)
    {
        GameObject particleEffect = factory.CreateParticleEffect(type);       
        return InstantiateParticleSystem(particleEffect);
    }

    private GameObject InstantiateParticleSystem(GameObject toInit)
    {
        GameObject newObj = GameObject.Instantiate(toInit);
        ParticleEffectReusable reusePartEffect = newObj.GetComponent<ParticleEffectReusable>();
        reusePartEffect.StartEffect();
        reusePartEffect.IsPlaying = true;
        particleEffects.Add(reusePartEffect);
        return newObj;
    }


    public void ClearUpNulls()
    {
        if (particleEffects.Count > 0)
        {
            for (int i = particleEffects.Count - 1; i >= 0; i--)
            {
                if (particleEffects[i] == null)
                {
                    particleEffects.Remove(particleEffects[i]);
                }
            }
        }

    }


    public void PreInitialize()
    {
        particleEffects = new List<ParticleEffectReusable>();
        factory = new ParticleEffectFactory();
    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
        for (int i = particleEffects.Count - 1 ; i >= 0 ; i--)
        {
            if(particleEffects[i].IsPlaying == false)
            {
                factory.GiveToPool(particleEffects[i].gameObject);
            }
        }
    }

    public void PhysicsRefresh()
    {
    }


    //public void Refresh()
    //{
    //    ClearUpNulls();
    //}

}
