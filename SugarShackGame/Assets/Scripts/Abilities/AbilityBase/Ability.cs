using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public enum AbilityType { MysteryDrink, Horn, Steal, Syrup, BucketCannotSpill, Trampoline, Apple, IceBall, GiantSnowBall, SnowBall};

public abstract class Ability : ScriptableObject
{
    public virtual AbilityType type { get; set; }
    public virtual Sprite sprite { get; set; }
    public virtual SoundListEnum sound { get; set; }
    public virtual bool isThrowable { get; set; }

    public virtual float timeBeforeDestruction => 10.0f;

    public virtual bool Activate(Player player)
    {
        if (isThrowable && !player.throwerComponent.IsHoldingThrowable)
        {
            return true;
        }
        else if (!isThrowable && !player.recieverComponent.IsHoldingUnThrowable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void SetSprite(Sprite newSprite)
    {
        sprite = newSprite;
    }

    public void PlaySound()
    {
        SoundManager.Play?.Invoke(sound);
    }

    public virtual void PlaySpecialEffect()
    {
       
    }
}