using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGamePlay : MonoBehaviour
{
    public Canvas Gameplay;
    public Canvas Pause;
    public GameObject pauseFirstBtn;
    public Viewport[] viewports;
    public GameObject[] nameColors;

    public TMP_Text timerMin_TextBox;
    public TMP_Text timerSec_TextBox;
    public PlayerControls actions;

    private List<Player> players;
    private List<PlayerInput> pi;
    private List<CustomInputHandler> inputHandlers;
    private Sprite empty;

    float countdown;
    bool gameOnPause = false;
    private float pauseDeltaTimeout = 0.5f;
    bool canUpdateUISettings = false;

    private void Start()
    {
        Cursor.visible = false;
        countdown = UIManager.Instance.gameDuration;

        LoadResources();
        DisplayUI();
        InitActions();

    }

    #region Init
    private void LoadResources()
    {
        empty = Resources.Load<Sprite>("Sprites/Abilities/Empty");
    }
    private void InitWorldUIPlayerSettings()
    {
        if (FindObjectOfType<PlayerInputManager>().playerCount == 2)
            canUpdateUISettings = true;

        if (canUpdateUISettings)
        {
            players = PlayerManager.Instance.players;
            pi = new List<PlayerInput>();
            inputHandlers = new List<CustomInputHandler>();

            for (int i = 0; i < players.Count; i++)
            {
                pi.Add(players[i].playerInput);
                inputHandlers.Add(pi[i].gameObject.GetComponent<CustomInputHandler>());

                viewports[i].LinkPlayer(players[i]);
                nameColors[i].GetComponent<MeshRenderer>().materials[0].color = players[i].color;
                pi[i].gameObject.GetComponent<CustomInputHandler>().OnPauseAction += Pause_performed;
            }
        }
    }

    private void InitActions()
    {
        actions = new PlayerControls();
    }

    private void DisplayUI()
    {
        Pause.gameObject.SetActive(false);
        Gameplay.gameObject.SetActive(true);
    }
    #endregion
    public void Pause_performed()
    {
        Pause.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Pause.GetComponent<PauseView>().OnPause();
    }

    private void Update()
    {
        InitWorldUIPlayerSettings();

        //Checking what is the value of Pause in CustomInputHandler
        PauseCheck();

        if (!gameOnPause)
        {
            UpdateTime();

            if (canUpdateUISettings)
                UpdatePlayerUI();
        }

    }

    public void PauseCheck()
    {
        for (int i = 0; i < inputHandlers.Count; i++)
        {
            if (inputHandlers[i].Pause && !gameOnPause)
            {
                Pause_performed();
                gameOnPause = true;
            }
        }

        if (!Pause.GetComponent<PauseView>().paused)
        {
            gameOnPause = false;
        }
    }

    public void UpdateTime()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            double d = System.Math.Round(countdown, 2);
            float min = Mathf.FloorToInt(countdown / 60);
            float sec = Mathf.FloorToInt(countdown % 60);
            timerMin_TextBox.text = min.ToString("00");
            timerSec_TextBox.text = sec.ToString("00");
        }

        else
        {
            ResetCountdown();
            UIManager.Instance.LoadNextScene();
        }
    }

    private void ResetCountdown()
    {
        countdown = UIManager.Instance.gameDuration;
    }


    public void UpdatePlayerUI()
    {
        for (int i = 0; i < viewports.Length; i++)
        {
            #region Slots
            Sprite[] newSprites = new Sprite[2];

            if (players[i].abilityHandler.abilitySlots[0].sprite != null)
            {
                newSprites[0] = players[i].abilityHandler.abilitySlots[0].sprite;
            }
            else
            {
                newSprites[0] = empty;
            }
            if (players[i].abilityHandler.abilitySlots[1].sprite != null)
            {
                newSprites[1] = players[i].abilityHandler.abilitySlots[1].sprite;
            }
            else
            {
                newSprites[1] = empty;
            }

            viewports[i].RefreshSlots(newSprites);
            #endregion
            #region Buckets
            viewports[i].RefreshBucket(players[i].playerBucket.sapAmount);
            #endregion
            #region Syrup
            viewports[i].RefreshSyrup(players[i].syrupCanManager.GetCanCount());
            #endregion
        }
    }
}