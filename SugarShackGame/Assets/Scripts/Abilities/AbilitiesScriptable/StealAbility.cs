using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealAbility : Ability
{
    public override AbilityType type { get => AbilityType.Steal; set => base.type = value; }
}
