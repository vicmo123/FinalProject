using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAbility : Ability
{
    public override void Activate()
    {
        base.Activate();
        Debug.Log("StatusAbility Activate function");
    }

}
