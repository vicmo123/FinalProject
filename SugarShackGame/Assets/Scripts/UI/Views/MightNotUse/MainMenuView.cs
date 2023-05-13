using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Button[] buttons;
    private Button[] cmtButtons;
    private int currentSelection = 0;
    [HideInInspector]
    public PlayerControls actions;
    
    private void Start()
    {
        actions = new PlayerControls();

        cmtButtons = new Button[3];
        for (int i = 0; i < buttons.Length; i++)
        {
            cmtButtons[i]  = buttons[i].GetComponent<Button>();
            Debug.Log(buttons[i]);
        }
        buttons[currentSelection].Select();

        cmtButtons[0].onClick.AddListener(StartGame);
        cmtButtons[1].onClick.AddListener(GoToControlsView);
        cmtButtons[2].onClick.AddListener(GoToCreditsView);

        actions.UI_Navigation.Submit.performed += Submit_performed;
        actions.UI_Navigation.Up.performed += Up_performed;
        actions.UI_Navigation.Down.performed += Down_performed;
        actions.UI_Navigation.Enable();
    }

    private void Down_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Down");
        currentSelection++;
        currentSelection %= buttons.Length;
        buttons[currentSelection].Select();
    }

    private void Up_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Up");
        currentSelection--;
        if (currentSelection == -1)
        {
            currentSelection = buttons.Length - 1;
        }
        buttons[currentSelection].Select();
    }

    private void Submit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Submit");
        cmtButtons[currentSelection].onClick.Invoke();
    }

    public void StartGame()
    {
        UIManager.Instance.LoadNextScene();
    }
    public void GoToCreditsView()
    {
        UIManager.Instance.LoadOneScene(ScenesNames.Credits);
    }
    public void GoToControlsView()
    {
        UIManager.Instance.LoadOneScene(ScenesNames.Controls);
    }
}
