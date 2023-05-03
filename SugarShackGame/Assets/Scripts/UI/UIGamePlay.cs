using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIGamePlay : MonoBehaviour
{
    public Canvas Gameplay;
    public Canvas Pause;
    public Camera debug_Camera;

    private List<Player> players;
    private List<PlayerInput> pi;

    public TMP_Text timerMin_TextBox;
    public TMP_Text timerSec_TextBox;
    public PlayerControls actions;
    float countdown;
    bool gameOnPause = false;
    const float GAME_DURATION = 320;

    private void Start()
    {
        players = PlayerManager.Instance.players;
        pi = new List<PlayerInput>();
        foreach (var item in players)
        {
            pi.Add(item.GetComponent<PlayerInput>());
        }
        //Turn off debug camera
        debug_Camera.gameObject.SetActive(false);
        //Display the right UI
        Pause.gameObject.SetActive(false);
        Gameplay.gameObject.SetActive(true);
        //Display timer
        countdown = GAME_DURATION;

        //Init actions :

        actions = new PlayerControls();
        actions.Player.Pause.performed += Pause_performed;
        actions.Player.Enable();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("PAUSE");
        gameOnPause = true;
        Pause.gameObject.SetActive(true);
        actions.UI_Navigation.Enable();
        actions.Player.Disable();
        for (int i = 0; i < pi.Count; i++)
        {
            pi[i].SwitchCurrentActionMap("UI_Navigation");
        }
    }

    private void Update()
    {
        if (!gameOnPause)
            UpdateTime();
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
}
