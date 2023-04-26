using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAbility : Ability
{


    public override void Activate(Player player)
    {
        base.Activate();
        Debug.Log("ThrowableAbility activation");
        ThrowableManager.Instance.TryAddObjectToCollection(type, player.throwerComponent);     
    }
}
