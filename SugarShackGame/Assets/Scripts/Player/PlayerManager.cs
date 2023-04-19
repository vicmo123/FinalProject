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

    private LayerMask[] playerCamMasks;
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
        playerInputManager.playerJoinedEvent.AddListener((input) => {
            CreatePlayer(input);
        });

        factory = new PlayerFactory();
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

        ////For demo
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    currentBeardIndex++;
        //    if (currentBeardIndex >= factory.beardColors.Length)
        //    {
        //        currentBeardIndex = 0;
        //    }

        //    factory.ChangePlayerColor(ref player1, factory.beardColors[currentBeardIndex], factory.shirtColors[currentShirtIndex]);
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    currentShirtIndex++;
        //    if (currentShirtIndex >= factory.shirtColors.Length)
        //    {
        //        currentShirtIndex = 0;
        //    }

        //    factory.ChangePlayerColor(ref player1, factory.beardColors[currentBeardIndex], factory.shirtColors[currentShirtIndex]);
        //}
    }

    public void PhysicsRefresh()
    {
        foreach (var player in players)
        {
            player.PhysicsRefresh();
        }
    }

    public void CreatePlayer(PlayerInput input)
    {

        Player newPlayer = null;

        input.gameObject.transform.position = Vector3.zero;
        newPlayer = input.gameObject.GetComponent<Player>();
        factory.ChangePlayerColor(ref newPlayer, factory.beardColors[currentBeardIndex], factory.shirtColors[currentShirtIndex]);

        players.Add(newPlayer);

        int layerToAdd = (int)Mathf.Log(playerCamMasks[players.Count - 1].value, 2);
        newPlayer.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        newPlayer.GetComponentInChildren<CinemachineBrain>().gameObject.layer = layerToAdd;
        newPlayer.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;

        newPlayer.PreInitialize();
        newPlayer.Initialize();
    }
}
