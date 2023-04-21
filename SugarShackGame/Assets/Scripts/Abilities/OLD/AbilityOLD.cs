using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityOLD : ScriptableObject
{
    public Sprite sprite;
    public string abilityName;
    public float activeTime;
    public float cooldownTime;
    //NEW
    public bool isThowable; // If throwable, use the logic of a snowball, if not : prefab falling on player's head
    public GameObject prefab;
    public float velocity;

    public AbilityOLD() { }

    public AbilityOLD(string name, Sprite sprite, float activeTime, float cooldownTime)
    {
        this.abilityName = name;
        this.sprite = sprite;
        this.activeTime = activeTime;
        this.cooldownTime = cooldownTime;
    }

    //Wouldn't need this :
    public virtual void Activate()
    {
        Debug.Log("Base of ability called");
    }
}