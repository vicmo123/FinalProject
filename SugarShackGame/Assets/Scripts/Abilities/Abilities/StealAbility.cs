using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealAbility : Ability
{
    public override void Activate(Player player)
    {
        base.Activate(player);
        if(ThrowableManager.Instance.TryAddObjectToCollection(type, player.throwerComponent))
        {
            //use power of ability and remove from slot
        }

    }

    
}
