using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class UIGamePlay : MonoBehaviour
{
    public Canvas Gameplay;
    public Canvas Pause;
    public Camera debug_Camera;
    public Viewport[] viewports;

    private List<Player> players;
    private List<PlayerInput> pi;
    private List<Sprite> sprites;
    private Sprite empty;

    public TMP_Text timerMin_TextBox;
    public TMP_Text timerSec_TextBox;
    public PlayerControls actions;

    private Dictionary<AbilityType, Sprite> abilities;

    float countdown;
    bool gameOnPause = false;
    bool playersSet = false;
    const float GAME_DURATION = 320;

    private void Start()
    {
        Cursor.visible = false;
        //Turn off debug camera
       // debug_Camera.gameObject.SetActive(false);
        //Init Timer
        countdown = GAME_DURATION;

        LoadResources();
        DisplayUI();
        InitActions();

    }

    #region Init
    private void LoadResources()
    {
        //Load UI resources :
        abilities = new Dictionary<AbilityType, Sprite>();
        sprites = Resources.LoadAll<Sprite>("Sprites/Abilities").ToList();

        List<AbilityType> abilityNames = Enum.GetValues(typeof(AbilityType)).Cast<AbilityType>().ToList();


        for (int i = 0; i < sprites.Count; i++)
        {
            if (sprites[i].name.Equals("Empty"))
            {
                empty = sprites[i];
            }
            for (int j = 0; j < abilityNames.Count; j++)
            {
                if (sprites[i].name.Equals(abilityNames[j].ToString()))
                {
                    abilities.Add(abilityNames[j], sprites[i]);
                    continue;
                }

            }
        }
    }
    private void InitPlayers()
    {
        //Init Players
        if (PlayerManager.Instance.players.Count == 2 && !playersSet)
        {
            players = PlayerManager.Instance.players;
            pi = new List<PlayerInput>();
            foreach (var item in players)
            {
                pi.Add(item.GetComponent<PlayerInput>());
            }
            for (int i = 0; i < viewports.Length; i++)
            {
                viewports[i].LinkPlayer(players[i]);
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
        Debug.Log("PAUSE");
        
        Pause.gameObject.SetActive(true);
        Pause.GetComponent<PauseView>().OnPause();
    }

    private void Update()
    {
        InitPlayers();

        if (!gameOnPause)
        {
            UpdateTime();

            if (playersSet)
                UpdateSlots();
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
        countdown = GAME_DURATION;
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

    public void UpdateSlots()
    {
        Debug.Log("update slots");
        for (int i = 0; i < viewports.Length; i++)
        {
            Sprite[] newSprites = new Sprite[2];
            //newSprites[0] = this.sprites[0];
            //newSprites[1] = this.sprites[4];

            if (players[i].abilityHandler.abilitySlots[0].sprite != null)
            {

                Debug.Log("Slots 0 conatins : " + players[i].abilityHandler.abilitySlots[0].sprite.name);
                newSprites[0] = players[i].abilityHandler.abilitySlots[0].sprite;
            }
            else
            {
                newSprites[0] = empty;
            }
            if (players[i].abilityHandler.abilitySlots[1].sprite != null)
            {
                Debug.Log("Slots 1 conatins : " + players[i].abilityHandler.abilitySlots[0].sprite.name);
                newSprites[1] = players[i].abilityHandler.abilitySlots[1].sprite;
            }
            else
            {
                newSprites[1] = empty;
            }

            viewports[i].RefreshSlots(newSprites);
        }
    }
}

//NOTES FOR LATER :

//    playerInput.actions.FindActionMap("Actions2").Enable();