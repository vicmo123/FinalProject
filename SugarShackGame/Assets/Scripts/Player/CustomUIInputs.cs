using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomUIInputs : MonoBehaviour
{
    [HideInInspector]
    public Vector2 UIMove { get; private set; } = Vector2.zero;
    [HideInInspector]
    public bool Submit { get; private set; } = false;

    private bool isControlsBlocked = false;


    public void OnUIMove(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            UIMove = context.ReadValue<Vector2>();
        else
            UIMove = Vector2.zero;
        Debug.Log("Value of OnUIMove " + UIMove);
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!isControlsBlocked)
            Submit = context.action.triggered;
        else
            Submit = false;
        Debug.Log("Value of OnSubmit " + Submit);

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
