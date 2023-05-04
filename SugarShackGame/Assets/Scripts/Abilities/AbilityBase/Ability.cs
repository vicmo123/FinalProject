using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu(menuName = "Stats/Ability")]
public class Ability : ScriptableObject
{
    public AbilityType type;
    public Sprite sprite;
    public SoundListEnum sound;
    public bool isThrowable;
    public float timeBeforeDestruction;

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