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
    private PlayerInputManager pim;
    private List<Player> players;
    private PlayerInput[] pi;
    public TMP_Text timerMin_TextBox;
    public TMP_Text timerSec_TextBox;
    private PlayerControls actions;
    float countdown;
    bool gameOnPause = false;
    const float GAME_DURATION = 320;

    private void Start()
    {
        pi = FindObjectsOfType<PlayerInput>();
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
        actions.UI_Navigation.Enable();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        gameOnPause = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause.gameObject.SetActive(true);
            gameOnPause = true;
        }
        
        if (!gameOnPause)
            UpdateTime();
    }

    public void UpdateTime()
    {
        if (countdown > 0 & !gameOnPause)
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
            UIManager.Instance.LoadNextScene();
            ResetCountdown();
        }
    }

    private void ResetCountdown()
    {
        countdown = GAME_DURATION;
    }

    public void SetGameOnPause(bool value)
    {
        gameOnPause = value;
    }

    public void Resume()
    {
        gameOnPause = false;
    }
}
