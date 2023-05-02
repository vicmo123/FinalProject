using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Manager(typeof(PlayerManager))]
public class PlayerManager : IFlow
{
    #region Singleton
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerManager();
            }
            return instance;
        }
    }

    private PlayerManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    public LayerMask[] playerCamMasks;
    PlayerInputManager playerInputManager;
    PlayerFactory factory;
    public List<Player> players { get; private set; }



    //Temp for demo
    int currentBeardIndex = 0;
    int currentShirtIndex = 0;

    public void PreInitialize()
    {
        playerCamMasks = new LayerMask[2];
        playerCamMasks[0] = LayerMask.GetMask("Player1");
        playerCamMasks[1] = LayerMask.GetMask("Player2");

        players = new List<Player>();

        playerInputManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputManager>();
        playerInputManager.onPlayerJoined += (input) =>
        {
            InitializePlayer(input);
        };
        factory = new PlayerFactory("Prefabs/Player/Player");
    }

    public void Initialize()
    {
        
    }

    public void Refresh()
    {
        foreach (var player in players)
        {
            player.Refresh();
        }
    }

    public void PhysicsRefresh()
    {
        foreach (var player in players)
        {
            player.PhysicsRefresh();
        }
    }

    public void InitializePlayer(PlayerInput input)
    {
        Player generatedPlayer = input.gameObject.GetComponent<Player>();
        factory.ChangePlayerColor(ref generatedPlayer, factory.beardColors[currentBeardIndex], factory.shirtColors[currentShirtIndex]);
        players.Add(generatedPlayer);

        FixCinemachineCam(players[players.Count - 1]);

        players[players.Count - 1].PreInitialize();
        players[players.Count - 1].Initialize();
        players[players.Count - 1].SpawnAtLocation(new Vector3(10, 0, 5));

        currentBeardIndex++;
        currentShirtIndex++;
    }

    private void FixCinemachineCam(Player player)
    {
        int layerToAdd = (int)Mathf.Log(playerCamMasks[players.Count - 1].value, 2);
        player.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<CinemachineBrain>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
    }
}
