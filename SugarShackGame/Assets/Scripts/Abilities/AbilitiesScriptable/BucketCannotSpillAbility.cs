using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketCannotSpillAbility : Ability
{
    public override AbilityType type { get => AbilityType.BucketCannotSpill; set => base.type = value; }
}
