using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornAbility : Ability
{
    public override AbilityType type { get => AbilityType.Horn; set => base.type = value; }
    public override float timeBeforeDestruction => 1.0f;
}
