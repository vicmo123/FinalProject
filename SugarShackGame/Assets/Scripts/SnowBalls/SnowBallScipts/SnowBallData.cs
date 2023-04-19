using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/SnowBall")]
public class SnowBallData : ScriptableObject
{
    public SnowBallTypes type;
    public Material ballMat;
    
    public float speed;
    public float maxSpeed;
}
