using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class CustomInputHandler : MonoBehaviour
{
    [HideInInspector]
    public Vector2 move = Vector2.zero;
    [HideInInspector]
    public Vector2 look = Vector2.zero;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool sprint = false;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private void Update()
    {
        Debug.Log(move);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
