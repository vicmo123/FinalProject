using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleAbility : Ability
{
    public override AbilityType type { get => AbilityType.Apple; set => base.type = value; }
}
