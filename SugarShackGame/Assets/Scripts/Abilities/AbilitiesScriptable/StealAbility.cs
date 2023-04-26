using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealAbility : Ability
{
<<<<<<< HEAD:SugarShackGame/Assets/Scripts/Abilities/Abilities/StealAbility.cs
    public override void Activate(Player player)
    {
        base.Activate(player);
        if(ThrowableManager.Instance.TryAddObjectToCollection(type, player.throwerComponent))
        {
            //use power of ability and remove from slot
        }

    }

    
=======
    public override AbilityType type { get => AbilityType.Steal; set => base.type = value; }
>>>>>>> 17a14d23657b327dcba3a2eae271288ba8a22c72:SugarShackGame/Assets/Scripts/Abilities/AbilitiesScriptable/StealAbility.cs
}
