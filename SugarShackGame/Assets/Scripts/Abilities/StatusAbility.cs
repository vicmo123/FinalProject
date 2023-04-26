using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAbility : Ability
{
    public override void Activate(Player player)
    {
        base.Activate(player);
        Debug.Log("StatusAbility Activate function");
    }

}
