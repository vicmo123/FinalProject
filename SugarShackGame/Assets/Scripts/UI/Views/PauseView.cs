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
    public PlayerControls actions;

    private PlayerInputManager playerInputManager;

    private List<PlayerInput> inputs;
    private List<CustomInputHandler> customInputs;
    
    public bool paused = false;

    private void Start()
    {
        InitActions();

        buttons[0].GetComponent<Button>().onClick.AddListener(() => DePause());
        buttons[1].GetComponent<Button>().onClick.AddListener(() => Exit());
    }

    private void InitActions()
    {
        inputs = new List<PlayerInput>();
        customInputs = new List<CustomInputHandler>();
        playerInputManager = GameObject.FindGameObjectWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
      

        for (int i = 0; i < playerInputManager.playerCount; i++)
        {
            inputs.Add(playerInputManager.transform.GetChild(i).gameObject.GetComponent<PlayerInput>());
            customInputs.Add(inputs[i].gameObject.GetComponent<CustomInputHandler>());
        }
        actions = new PlayerControls();
    }

    private void ExitActions()
    {
        actions.UI_Navigation.Disable();
    }

    
    public void OnPause()
    {
        mainEntry.isPaused = true;
        paused = true;
    }

    public void DePause()
    {
        //Delay for no jumping when we get back in the scene
        StartCoroutine(DelayPlayerActionsInput());      
    }

    private IEnumerator DelayPlayerActionsInput()
    {
        yield return null;  // wait a frame

        ActionsOnPause();
    }

    private void ActionsOnPause()
    {
        StopFromJumping();
        mainEntry.isPaused = false;
        mainEntry.isPauseFinished = true;
        paused = false;

        for (int i = 0; i < inputs.Count; i++)
        {
            inputs[i].actions.FindActionMap("Player").Enable();
            inputs[i].actions.FindActionMap("UI_Navigation").Disable();
        }
        this.gameObject.SetActive(false);
    }

    private void Exit()
    {
        ExitActions();
        mainEntry.isExit = true;
        paused = false;
    }

    private void StopFromJumping()
    {
        for (int i = 0; i < customInputs.Count; i++)
        {
            customInputs[i].Jump = false ;
        }
    }
}
