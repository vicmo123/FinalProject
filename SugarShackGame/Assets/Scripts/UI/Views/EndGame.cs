using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Button button;
    private Button cmptButton;
    private PlayerControls actions;

    private void Start()
    {
        Debug.Log("End Game Scene");
        actions = new PlayerControls();
        cmptButton = button.GetComponent<Button>();
        cmptButton.onClick.AddListener(TaskOnClick);
        actions.UI_Navigation.Submit.performed += Submit_performed1;
        Cursor.visible = false;
    }

    private void Submit_performed1(InputAction.CallbackContext obj)
    {
        button.onClick.Invoke();
    }


    void TaskOnClick()
    {
        Debug.Log("Task On click in Controls");
        UIManager.Instance.LoadOneScene(ScenesNames.MainMenu);
    }
}
