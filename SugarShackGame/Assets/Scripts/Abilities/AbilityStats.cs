using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Ability")]
public class AbilityStats : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string abilityName;
    public bool isThowable; // If throwable, use the logic of a snowball, if not : prefab falling on player's head
    [Range(0, 100)] public float activeTime;
    [Range(0, 100)] public float cooldownTime;
    [Range(0, 100)] public float velocity;

}
