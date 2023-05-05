using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    public MainEntry mainEntry;
    public List<Button> buttons;
    public Action<InputActionMap> inputActions;
    public PlayerControls actions;

    private PlayerInputManager playerInputManager;
    private List<PlayerInput> pi;
    private int currentSelection = 0;

    private void Start()
    {
        InitActions();
        EventSystem.current.SetSelectedGameObject(null);
        buttons[0].GetComponent<Button>().onClick.AddListener(() => DePause());
        buttons[1].GetComponent<Button>().onClick.AddListener(() => Exit());
    }

    private void InitActions()
    {
        pi = new List<PlayerInput>();
        playerInputManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputManager>();
        playerInputManager.onPlayerJoined += (input) =>
        {
            pi.Add(input);
        };
        actions = new PlayerControls();
        actions.UI_Navigation.Submit.performed += Submit_performed;
        actions.UI_Navigation.Up.performed += Up_performed;
        actions.UI_Navigation.Down.performed += Down_performed;

    }

    private void ExitActions()
    {
        actions.UI_Navigation.Submit.performed -= Submit_performed;
        actions.UI_Navigation.Up.performed -= Up_performed;
        actions.UI_Navigation.Down.performed -= Down_performed;
        actions.UI_Navigation.Disable();
    }

    private void Submit_performed(InputAction.CallbackContext obj)
    {
        buttons[currentSelection].GetComponent<Button>().onClick.Invoke();
    }

    private void Up_performed(InputAction.CallbackContext obj)
    {
        currentSelection--;
        if (currentSelection == -1)
        {
            currentSelection = buttons.Count - 1;
        }
        buttons[currentSelection].Select();
    }

    private void Down_performed(InputAction.CallbackContext obj)
    {
        currentSelection++;
        currentSelection %= buttons.Count;
        buttons[currentSelection].Select();
    }

    public void OnPause()
    {
        mainEntry.isPaused = true;
        actions.UI_Navigation.Enable();
        actions.Player.Disable();
    }

    public void DePause()
    {
        mainEntry.isPaused = false;
        actions.UI_Navigation.Disable();
        actions.Player.Enable();
        mainEntry.isPauseFinished = true;
    }

    private void Exit()
    {
        ExitActions();
        mainEntry.isExit = true;
    }
    //private void OnEnable()
    //{
    //    Debug.Log("Pause View was displayed.");
    //    //actions.UI_Navigation.Enable();
    //    //actions.Player.Disable();
    //    //IN PROCESS
    //}

    //private void OnDisable()
    //{
    //    Debug.Log("Pause menu was disabled");
    //}
}
