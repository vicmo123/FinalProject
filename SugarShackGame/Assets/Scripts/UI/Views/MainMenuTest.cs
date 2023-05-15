using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuTest : MonoBehaviour
{
    public GameObject MainMenu, Controls, Credits;
    public GameObject mainMenuFirstBtn, controlsFirstBtn, creditsFirstBtn;
    private GameObject currentView;

    // Start is called before the first frame update
    void Start()
    {
        currentView = MainMenu;
        MainMenu.SetActive(true);
        Controls.SetActive(false);
        Credits.SetActive(false);
    }

    private void Update()
    {
        if(currentView != MainMenu)
        {
            Debug.Log(Input.GetButtonDown("CancelButton"));
        }

        if (Input.GetButtonDown("CancelButton"))
        {
            Debug.Log("CancelButton");
        }
    }

    public void SwitchView(GameObject nextCanvas, GameObject firstBtn)
    {
        nextCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstBtn);
        currentView.SetActive(false);
        currentView = nextCanvas;
    }


    public void StartBtn()
    {
        UIManager.Instance.LoadNextScene();
    }

    public void ControlsBtnPressed()
    {
        SwitchView(Controls, controlsFirstBtn);
    }

    public void PreviousBtnPressed()
    {
        SwitchView(MainMenu, mainMenuFirstBtn);
    }

    public void CreditsBtnPressed()
    {
        SwitchView(Credits, creditsFirstBtn);
    }

    public void HiddenBtnPressed()
    {
        SwitchView(MainMenu, mainMenuFirstBtn);
    }


    
}
