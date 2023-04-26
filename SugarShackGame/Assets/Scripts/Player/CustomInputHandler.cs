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
    public Vector2 move { get; private set; } = Vector2.zero;
    [HideInInspector]
    public Vector2 look { get; private set; } = Vector2.zero;
    [HideInInspector]
    public bool jump { get; set; } = false; // For not braking movement script
    [HideInInspector]
    public bool sprint { get; private set; } = false;
    [HideInInspector]
    public bool Throw { get; set; } = false;
    [HideInInspector]
    public bool Use { get; private set; } = false;
    [HideInInspector]
    public bool UseLeftPowerUp { get; set; } = false;
    [HideInInspector]
    public bool UseRightPowerUp { get; set; } = false;
    [HideInInspector]
    public bool Aim { get; private set; } = false;
    [HideInInspector]
    public bool Pause { get; private set; } = false;




    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    #region CheckInput
    //These must be here to work for some reason, doenst work in custom handler.
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.action.triggered;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        sprint = context.action.triggered;
    }

    public void OnTest(InputAction.CallbackContext context)
    {
        Debug.Log("Test called on " + gameObject.name + " !");
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        Throw = context.action.triggered;
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        Use = context.action.triggered;
    }

    public void OnLeftPowerUp(InputAction.CallbackContext context)
    {
        UseLeftPowerUp = context.action.triggered;
    }

    public void OnRightPowerUp(InputAction.CallbackContext context)
    {
        UseRightPowerUp = context.action.triggered;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Aim = context.action.triggered;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Pause = context.action.triggered;
    }
    #endregion

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
