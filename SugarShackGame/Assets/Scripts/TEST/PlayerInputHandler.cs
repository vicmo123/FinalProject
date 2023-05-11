using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private CustomInputHandler customInputHandler;

    //to change the color
    private int indexBeard;
    private int indexShirt;

    private PlayerControls controls;

    [HideInInspector]
    public Vector2 Move { get; private set; } = Vector2.zero;

    private bool isControlsBlocked = false;

    private void Awake()
    {
        controls = new PlayerControls();

        customInputHandler = this.transform.gameObject.GetComponent<CustomInputHandler>();
    }

    public void InitializePlayer(PlayerConfiguration _playerConfig)
    {
        playerConfig = _playerConfig;
        indexBeard = playerConfig.IndexColorBeard;
        indexShirt = playerConfig.IndexColorShirt;

    }

    public void RegisterToActions()
    {
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;

    }


    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Movement.name)
        {
            Debug.Log("An action was triggered!!!!!");
            if (!isControlsBlocked)
                Move = obj.ReadValue<Vector2>();
            else
                Move = Vector2.zero;
        }
        else if (obj.action.name == controls.Player.Jump.name)
        {
            customInputHandler.OnJump(obj);
        }

    }
}
