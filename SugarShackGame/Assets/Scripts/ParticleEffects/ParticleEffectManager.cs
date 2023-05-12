using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Manager(typeof(ParticleEffectManager))]
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
    
    private ParticleEffectFactory factory;


    public void PreInitialize()
    {
        factory = new ParticleEffectFactory();
    }
    public GameObject Create(ParticleEffectType type)
    {           
        return factory.CreateParticleEffect(type);
    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
        
    }

    public void PhysicsRefresh()
    {
    }

}
