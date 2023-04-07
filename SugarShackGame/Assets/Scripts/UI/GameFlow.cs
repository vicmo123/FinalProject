using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public Canvas pause_window;
    public Canvas endGame_window;
    public Canvas mainMenu_window;

    private void Start()
    {
        mainMenu_window.gameObject.SetActive(true);
        pause_window.gameObject.SetActive(false);
        endGame_window.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause_window.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            endGame_window.gameObject.SetActive(true);
        }
    }
}