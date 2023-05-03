using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseView : MonoBehaviour
{
    public Action<InputActionMap> inputActions;
    public PlayerControls actions;

    private void Start()
    {
        actions = new PlayerControls();
    }
    private void OnEnable()
    {
        Debug.Log("Pause View was displayed.");
        actions.UI_Navigation.Enable();
        actions.Player.Disable();
        //IN PROCESS
    }

    private void OnDisable()
    {
        Debug.Log("Pause menu was disabled");
    }
}
