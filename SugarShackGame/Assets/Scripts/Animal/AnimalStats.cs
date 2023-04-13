using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Animal/Stats")]
public class AnimalStats : ScriptableObject
{
    [Range(0, 100)] public float walkSpeed;
    [Range(0, 100)] public float runSpeed;
    [Range(0, 100)] public float fovLenght;
    [Range(0, 100)] public float walkRadius;
}
