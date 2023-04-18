using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Manager(typeof(PlayerControllerManager))]
[RequireComponent(typeof(PlayerInputManager))]
public class PlayerControllerManager : MonoBehaviour, IFlow
{
    #region Singleton
    private static PlayerControllerManager instance;

    public static PlayerControllerManager Instance {
        get {
            if (instance == null) {
                instance = new PlayerControllerManager();
            }
            return instance;
        }
    }

    private PlayerControllerManager() {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    PlayerInputManager playerInputManager;
    public List<Transform> playerSpawns;

    public void JoinPlayer(PlayerInput input) {
        if (playerSpawns.Count < playerInputManager.playerCount + 1)
            Debug.LogError("Missing a player spawn inside of PlayerControllerManager!");
        else
            input.gameObject.transform.position = playerSpawns[playerInputManager.playerCount - 1].position;
    }

    public void LeavePlayer(PlayerInput input) {

    }

    public void Initialize() {
    }

    public void PhysicsRefresh() {
    }

    public void PreInitialize() {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void Refresh() {
    }
}
