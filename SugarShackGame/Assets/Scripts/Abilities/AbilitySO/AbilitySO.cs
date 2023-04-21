using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string abilityName;
    public float activeTime;
    public float cooldownTime;
    public bool isThowable; // If throwable, use the logic of a snowball, if not : prefab falling on player's head
    public float velocity;

    public AbilitySO(GameObject prefab, Sprite sprite, string abilityName, float activeTime, float cooldownTime, bool isThowable, float velocity)
    {
        this.sprite = sprite;
        this.abilityName = abilityName;
        this.activeTime = activeTime;
        this.cooldownTime = cooldownTime;
        this.isThowable = isThowable;
        this.prefab = prefab;
        this.velocity = velocity;
    }

    public GameObject GetAbilityPrefab()
    {
        return this.prefab;
    }
}
