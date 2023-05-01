using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDurationView : MonoBehaviour
{
    public Button opt1Button;
    public float timeOpt1;
    public Button opt2Button;
    public float timeOpt2;
    public Button opt3Button;
    public float timeOpt3;
    public Button startGameButton;
    private PlayerControls actions;

    private void Awake()
    {
        Button btn1 = opt1Button.GetComponent<Button>();
        Button btn2 = opt2Button.GetComponent<Button>();
        Button btn3 = opt3Button.GetComponent<Button>();
        Button startBtn = startGameButton.GetComponent<Button>();

        btn1.onClick.AddListener(() => SetGameDuration(timeOpt1));
        btn2.onClick.AddListener(() => SetGameDuration(timeOpt2));
        btn3.onClick.AddListener(() => SetGameDuration(timeOpt3));
        startBtn.onClick.AddListener(() => UIManager.Instance.LoadOneScene(ScenesNames.GamePlay));

        InitActions();
    }
    private void InitActions()
    {
        actions = new PlayerControls();        
        actions.UI_Settings_Duration.Up.performed += Up_performed;
        actions.UI_Settings_Duration.Down.performed += Down_performed;
        actions.UI_Settings_Duration.Submit.performed += Submit_performed;
        actions.UI_Settings_Duration.Enable();
    }

    private void Submit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Submit ");
    }

    private void Down_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Down");
    }

    private void Up_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Up");
    }

    public void SetGameDuration(float duration)
    {
        UIManager.Instance.gameDuration = duration;
        Debug.Log("Game duration is : " + duration);
    }

    
}
