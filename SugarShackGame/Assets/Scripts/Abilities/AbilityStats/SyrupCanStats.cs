using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/SyrupCanStats")]
public class SyrupCanStats : Ability
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
