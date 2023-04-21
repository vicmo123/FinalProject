using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityOLD))]
public class AbilityDrawer : Editor
{
    public SerializedProperty spriteProperty;
    public SerializedProperty abilityName;
    public SerializedProperty isActive;
    public SerializedProperty activeTime;
    public SerializedProperty cooldownTime;

    private SerializedObject ability;
    


    public void OnEnable()
    {
        ability = new SerializedObject(target);
        spriteProperty = ability.FindProperty("sprite");
        abilityName = ability.FindProperty("abilityName");
        isActive = ability.FindProperty("isActive");
        activeTime = ability.FindProperty("activeTime");
        cooldownTime = ability.FindProperty("cooldownTime");
    }

    public override void OnInspectorGUI()
    {
        Debug.Log("Override Inspector Ability");
        ability.Update();

        //GUILayout.Label("Card Assets", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(abilityName);
        EditorGUILayout.PropertyField(isActive);
        EditorGUILayout.PropertyField(activeTime);
        EditorGUILayout.PropertyField(spriteProperty);

        //Sprite sprite = (Sprite)spriteProperty.objectReferenceValue;
        //if (sprite != null)
        //{
        //    GUILayout.Label(sprite.texture, GUILayout.MaxHeight(300f));
        //}

        EditorGUILayout.PropertyField(cooldownTime);
        EditorGUILayout.EndVertical();

        ability.ApplyModifiedProperties();
    }
}
