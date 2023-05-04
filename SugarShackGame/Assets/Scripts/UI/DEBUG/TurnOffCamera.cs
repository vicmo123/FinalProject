using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOffCamera : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    private void Start()
    {
        playerInputManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputManager>();
        playerInputManager.onPlayerJoined += (input) =>
        {
            gameObject.SetActive(false);
        };

    }
}
