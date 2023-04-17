using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/AppleStats")]
public class AppleStats : Ability
{

    public AppleStats(string name, Sprite sprite, float activeTime, float cooldownTime) : base(name, sprite, activeTime, cooldownTime)
    {
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("AppleStats effect");
    }

}
