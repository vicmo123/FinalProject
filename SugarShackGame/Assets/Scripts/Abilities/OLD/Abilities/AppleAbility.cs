using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleAbility : AbilityOLD
{
    public AppleAbility(string name, Sprite sprite, float activeTime, float cooldownTime) : base(name, sprite, activeTime, cooldownTime)
    {
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("Apple effect");
    }

}
