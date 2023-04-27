using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryDrinkAbility : Ability
{
    public override AbilityType type { get => AbilityType.MysteryDrink; set => base.type = value; }
    public override float timeBeforeDestruction => 1.0f;
}
