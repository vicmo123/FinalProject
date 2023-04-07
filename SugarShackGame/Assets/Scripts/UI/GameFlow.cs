using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFlow : MonoBehaviour
{
    public Canvas pause_window;
    public Canvas endGame_window;
    public Canvas mainMenu_window;
    public TMP_Text timerMin_TextBox;
    public TMP_Text timerSec_TextBox;
    bool gameOnPause = false;
    float countdown;
    float gameDuration = 180;

    private void Start()
    {
        mainMenu_window.gameObject.SetActive(true);
        pause_window.gameObject.SetActive(false);
        endGame_window.gameObject.SetActive(false);
        countdown = gameDuration;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause_window.gameObject.SetActive(true);
            gameOnPause = true;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            endGame_window.gameObject.SetActive(true);
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
            endGame_window.gameObject.SetActive(true);
            countdown = 10;
        }
    }

    private void ResetCountdown()
    {
        countdown = gameDuration;
    }

    public void SetGameOnPause(bool value)
    {
        gameOnPause = value;
    }
}