using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Canvas credits;
    public Canvas controls;
    public Button startGameButton;
    public Button creditsButton;
    public Button controlsButton;

    private void Awake()
    {
        credits.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);       
    }
    private void Start()
    {
        Button startBtn = startGameButton.GetComponent<Button>();
        Button credBtn = creditsButton.GetComponent<Button>();
        Button contBtn = controlsButton.GetComponent<Button>();

        startBtn.onClick.AddListener(StartGame);
        credBtn.onClick.AddListener(GoToCreditsView);
        contBtn.onClick.AddListener(GoToControlsView);
    }

    public void StartGame()
    {
        Debug.Log("Starting the game");
        UIManager.Instance.LoadOneScene(ScenesNames.Setup);
    }
    public void GoToCreditsView()
    {
        credits.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void GoToControlsView()
    {
        controls.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
