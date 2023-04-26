using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Throwable")]
public class ThrowableData : ScriptableObject
{
    public AbilityType type;
    public float TimeBeforeDestruction = 10;
    public float Acceleration = 20;
    public float MaxSpeed = 300;
    public float ImpactMultiplier = 1;
}
