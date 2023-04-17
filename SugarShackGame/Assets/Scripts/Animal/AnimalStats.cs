using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Stats/Animal")]
public class AnimalStats : ScriptableObject
{
    //Movement
    [Range(0, 100)] public float walkSpeed;
    [Range(0, 100)] public float runSpeed;
    [Range(0, 100)] public float rotationSpeed;
    [Range(0, 100)] public float fovLenght;
    [Range(0, 100)] public float walkRadius;
    [Range(0, 100)] public int hp;

    //Sight
    public string targetTagName;
    [Range(0, 100)] public float viewRadius = 10f;
    [Range(0, 100)] public float viewAngle = 45f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
}
