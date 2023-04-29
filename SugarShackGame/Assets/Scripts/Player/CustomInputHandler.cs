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
    public Vector2 Move { get; private set; } = Vector2.zero;
    [HideInInspector]
    public Vector2 Look { get; private set; } = Vector2.zero;
    [HideInInspector]
    public bool Jump { get; set; } = false; // For not braking movement script
    [HideInInspector]
    public bool Sprint { get; private set; } = false;
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

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private bool isControlsBlocked = false;

    #region CheckInput
    //These must be here to work for some reason, doenst work in custom handler.
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Move = context.ReadValue<Vector2>();
        else
            Move = Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Jump = context.action.triggered;
        else
            Jump = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Look = context.ReadValue<Vector2>();
        else
            Look = Vector2.zero;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Sprint = context.action.triggered;
        else
            Sprint = false;
    }

    public void OnTest(InputAction.CallbackContext context)
    {
        Debug.Log("Test called on " + gameObject.name + " !");
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Throw = context.action.triggered;
        else
            Throw = false;
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Use = context.action.triggered;
        else
            Use = false;
    }

    public void OnLeftPowerUp(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            UseLeftPowerUp = context.action.triggered;
        else
            UseLeftPowerUp = false;
    }

    public void OnRightPowerUp(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            UseRightPowerUp = context.action.triggered;
        else
            UseRightPowerUp = false;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Aim = context.action.triggered;
        else
            Aim = false;
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

    public void BlockControls()
    {
        isControlsBlocked = true;
    }

    public void UnlockControls()
    {
        isControlsBlocked = false;
    }
}
