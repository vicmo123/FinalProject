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
    public MainEntry mainEntry;
    public Canvas Gameplay;
    public Canvas Pause;
    public PauseView pauseView;
    public GameObject pauseFirstBtn;
    public Viewport[] viewports;
    public GameObject[] nameColors;

    public TMP_Text timerMin_TextBox;
    public TMP_Text timerSec_TextBox;

    private List<Player> players;
    private List<PlayerInput> pi;
    private List<CustomInputHandler> inputHandlers;
    private Sprite empty;

    private float countdown;
    private bool gameOnPause = false;
    private bool gameOver = false;
    private float pauseDeltaTimeout = 0.5f;
    private bool canUpdateUISettings = false;

    private void Start()
    {
        Cursor.visible = false;
        countdown = UIManager.Instance.gameDuration;
        pauseView = Pause.gameObject.GetComponent<PauseView>();
        Debug.Log(pauseView.name);

        LoadResources();
        DisplayUI();
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
        pauseView.OnPause();
    }

    private void Update()
    {
        InitWorldUIPlayerSettings();

        //Checking value of Pause in CustomInputHandler
        PauseCheck();

        if (!gameOnPause && !gameOver)
        {
            UpdateTime();

            if (canUpdateUISettings)
                UpdatePlayerUI();
        }

        if(gameOver)
            mainEntry.isGameOver = true;
    }

    public void PauseCheck()
    {
        for (int i = 0; i < inputHandlers.Count; i++)
        {
            //allowing to trigger only once
            if (inputHandlers[i].Pause )
            {
                Pause_performed();
                gameOnPause = true;
            }
        }

        if (!pauseView.paused)
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
            if (min == 0 && sec == 0)
                gameOver = true;
            else
            {
                timerMin_TextBox.text = min.ToString("00");
                timerSec_TextBox.text = sec.ToString("00");
            }
        }

        else
        {
            gameOver = true;                    
        }
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