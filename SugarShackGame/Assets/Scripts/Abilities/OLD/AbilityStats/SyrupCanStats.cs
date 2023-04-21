using System.Collections.Generic;
using UnityEngine;

public class SyrupCanStats : AbilityOLD
{

    public SyrupCanStats(string name, Sprite sprite, float activeTime, float cooldownTime) : base(name, sprite, activeTime, cooldownTime)
    {
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("SyrupCanStats effect");
    }

}
