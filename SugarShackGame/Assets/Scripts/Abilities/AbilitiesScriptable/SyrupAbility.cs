using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupAbility : Ability
{
    public override AbilityType type { get => AbilityType.Syrup; set => base.type = value; }
    public override float timeBeforeDestruction => 1.0f;
}
