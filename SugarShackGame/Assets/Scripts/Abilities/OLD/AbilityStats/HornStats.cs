using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HornStats : AbilityOLD
{
    public HornStats(string name, Sprite sprite, float activeTime, float cooldownTime) : base(name, sprite, activeTime, cooldownTime)
    {
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("Horn effect");
    }
}