using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameDurationView : MonoBehaviour
{
    public List<Button> buttons;
    public List<float> durationsOfGame;

    private List<Button> btnComponents;
    private int currentSelection = 0;
    private PlayerControls actions;

    private void Awake()
    {
        btnComponents = new List<Button>();
        for (int i = 0; i < buttons.Count; i++)
        {
            Debug.Log(buttons[i]);
            Button btn = buttons[i].GetComponent<Button>();
            btnComponents.Add(btn);

            if (i == 3)
                btn.onClick.AddListener(() => UIManager.Instance.LoadNextScene());
            else
                btn.onClick.AddListener(() => SetGameDuration(durationsOfGame[i]));
        }
    }

    private void Start()
    {
        Cursor.visible = false;
    }
    private void InitActions()
    {
        actions.Enable();
        actions.UI_Navigation.Up.performed += Up_performed;
        actions.UI_Navigation.Down.performed += Down_performed;
        actions.UI_Navigation.Submit.performed += Submit_performed;
    }

    private void Submit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Submit ");
        btnComponents[currentSelection].onClick.Invoke();
    }

    private void Down_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Down");
        currentSelection++;
        currentSelection %= buttons.Count;
        buttons[currentSelection].Select();

    }

    private void Up_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Up");
        currentSelection--;
        if (currentSelection == -1)
        {
            currentSelection = buttons.Count - 1;
        }
        buttons[currentSelection].Select();
    }

    public void SetGameDuration(float duration)
    {
        UIManager.Instance.gameDuration = duration;
        Debug.Log("Game duration is : " + duration);
    }

    public void IsCalled(PlayerControls actions)
    {
        Debug.Log("GameDuration : IsCalled Function");

        this.actions = actions;
        buttons[0].Select();
        InitActions();
    }

}
