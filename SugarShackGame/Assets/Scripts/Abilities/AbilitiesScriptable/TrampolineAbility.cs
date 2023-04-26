using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineAbility : Ability
{
    public override AbilityType type { get => AbilityType.Trampoline; set => base.type = value; }
}
