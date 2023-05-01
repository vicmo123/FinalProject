using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        UIManager.Instance.LoadNextScene();
        //int index = UIManager.Instance.CurrentScene;
        //++index;
        //newScene(index);
        //UIManager.Instance.CurrentScene = index;
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


    public void newScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);

        if (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            StartCoroutine(waitForSceneLoad(sceneNumber));
        }
    }

    IEnumerator waitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
        }

        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().buildIndex == sceneNumber)
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
        }       
    }
}
