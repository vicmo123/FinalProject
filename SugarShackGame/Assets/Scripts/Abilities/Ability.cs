using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AbilityType { MysteryDrink, Horn, Steal, Syrup, BucketNotSpill, Trampoline, Apple, Iceball, GiantSnowBall, SnowBall};

public class Ability : MonoBehaviour
{
    [Header("Ability Links")]
    [SerializeField]
    protected AbilityType type;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected SoundListEnum sound;

    public virtual void Activate(Player player)
    {
        Debug.Log("Base of ability called");
        //s'attache a la main
       // if (ThrowableManager.Instance.TryAddObjectToCollection(type, player.receiver))
        

    }

    public Sprite GetSprite()
    {
        return sprite;
    }


    public void PlaySound()
    {
        SoundManager.Play?.Invoke(sound);
    }

    public virtual void PlaySpecialEffect()
    {
       
    }

}