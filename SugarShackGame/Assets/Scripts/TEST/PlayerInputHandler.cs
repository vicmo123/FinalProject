using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;

    //to change the color
    [SerializeField]
    private int indexBeard;
    [SerializeField]
    private int indexShirt;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfiguration _playerConfig)
    {
        playerConfig = _playerConfig;
        indexBeard = playerConfig.IndexColorBeard;
        indexShirt = playerConfig.IndexColorShirt;

        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
}
