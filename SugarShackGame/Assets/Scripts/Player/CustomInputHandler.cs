using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]

public class CustomInputHandler : MonoBehaviour
{
    [HideInInspector]
    public Vector2 Move { get; private set; } = Vector2.zero;
    [HideInInspector]
    public Vector2 Look { get; private set; } = Vector2.zero;
    [HideInInspector]
    public bool Jump { get; set; } = false; // For not breaking movement script
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
    [HideInInspector]
    public bool Pause { get; private set; } = false;

   

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
        Debug.Log("A move was triggered");
        if (!isControlsBlocked)
            Move = context.ReadValue<Vector2>();
        else
            Move = Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMPING!");
        if (!isControlsBlocked)
            Jump = context.action.triggered;
        else
            Jump = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("On Look!!!");
        if (context.control.device.name.Equals("mouse")){
            Debug.Log("A MOUSE MOVEMENT WAS REGISTERED");
        }

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
        //Debug.Log("Test called on " + gameObject.name + " !");
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        Debug.Log("THROWINGGGGGG!!!");
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
        Debug.Log("On Right POwer Up!");
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

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("ON Pause!");
        Pause = context.action.triggered;

    }

    public void OnUp(InputAction.CallbackContext context)
    {
        Debug.Log("UP");
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        Debug.Log("Down");
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        Debug.Log("Submit");
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
