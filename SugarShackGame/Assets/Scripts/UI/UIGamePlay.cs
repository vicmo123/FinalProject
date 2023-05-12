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
    private Sprite empty;

    float countdown;
    bool gameOnPause = false;
    bool playersSet = false;

    private void Start()
    {
        Cursor.visible = false;
        //Turn off debug camera
        // debug_Camera.gameObject.SetActive(false);
        //Init Timer
        countdown = UIManager.Instance.gameDuration;
        Debug.Log("time for the game : " + countdown);

        LoadResources();
        DisplayUI();
        InitActions();

    }

    #region Init
    private void LoadResources()
    {
        empty = Resources.Load<Sprite>("Sprites/Abilities/Empty");
    }
    private void InitPlayers()
    {
        //Init Players
        if (PlayerManager.Instance.players.Count == 2 && !playersSet)
        {
            players = PlayerManager.Instance.players;
            pi = new List<PlayerInput>();

            for (int i = 0; i < players.Count; i++)
            {
                pi.Add(players[i].playerInput);
                viewports[i].LinkPlayer(players[i]);
                nameColors[i].GetComponent<MeshRenderer>().materials[0].color = players[i].color;
            }
            playersSet = true;
        }
    }

    private void InitActions()
    {
        //Init actions :
        actions = new PlayerControls();
        actions.Player.Pause.performed += Pause_performed;
        actions.Player.Enable();
    }

    private void DisplayUI()
    {
        //Display the right UI
        Pause.gameObject.SetActive(false);
        Gameplay.gameObject.SetActive(true);
    }
    #endregion

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        Pause.gameObject.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstBtn);
        Pause.GetComponent<PauseView>().OnPause();
    }

    private void Update()
    {
        InitPlayers();

        if (!gameOnPause)
        {
            UpdateTime();

            if (playersSet)
                UpdatePlayerUI();
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

    public void Resume()
    {
        gameOnPause = false;
        Pause.gameObject.SetActive(false);
        actions.UI_Navigation.Disable();
        for (int i = 0; i < pi.Count; i++)
        {
            pi[i].SwitchCurrentActionMap("Player");
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
