using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player")]
public class PlayerStats : ScriptableObject
{
    //Movement
    [Range(0, 100)] public float walkSpeed;
    [Range(0, 100)] public float runSpeed;
}
