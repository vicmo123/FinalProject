using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AbilityType { MysteryDrink, Horn, Steal, Syrup, BucketNotSpill, Trampoline, Apple, Iceball, GiantBall};

public class Ability : MonoBehaviour, IFlow
{
    [Header("Ability Links")]
    [SerializeField]
    protected AbilityType type;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected SoundListEnum sound;

    public virtual void Activate()
    {
        Debug.Log("Base of ability called");
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public virtual void Initialize()
    {
    }

    public virtual void PhysicsRefresh()
    {
    }

    public void PlaySound()
    {
        SoundManager.Play?.Invoke(sound);
    }

    public virtual void PlaySpecialEffect()
    {
       
    }

    public virtual void PreInitialize()
    {
    }

    public virtual void Refresh()
    {
    }
}