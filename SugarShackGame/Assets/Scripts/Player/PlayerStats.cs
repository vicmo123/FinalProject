using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player")]
public class PlayerStats : ScriptableObject
{
    public float initialSpeed = 2.0f;
    public float maxWalkSpeed = 5.0f;
    public float maxRunSpeed = 10.0f;
    public float accelerationSpeed = 0.5f;
    
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    public float sensitivity = 3;
}
