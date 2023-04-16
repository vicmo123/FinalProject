using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ability : ScriptableObject
{
    public Sprite sprite;
    public string abilityName;
    public float activeTime;
    public float cooldownTime;

    public Ability() { }

    public Ability(string name, Sprite sprite, float activeTime, float cooldownTime)
    {
        this.abilityName = name;
        this.sprite = sprite;
        this.activeTime = activeTime;
        this.cooldownTime = cooldownTime;
    }

    public virtual void Activate()
    {
        Debug.Log("Base of ability called");
    }
}