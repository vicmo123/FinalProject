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

    public void CreatePlayer(PlayerInput input)
    {
        //int playerIndex = 0;
        //if (input.user.index == 1)
        //    playerIndex = 1;

        Player newPlayer = null;

        input.gameObject.transform.position = Vector3.zero;
        newPlayer = input.gameObject.GetComponent<Player>();
        factory.ChangePlayerColor(ref newPlayer, factory.beardColors[currentBeardIndex], factory.shirtColors[currentShirtIndex]);
        //newPlayer = factory.CreatPlayer( factory.beardColors[UIManager.Instance.players[playerIndex].indexBeard], factory.shirtColors[UIManager.Instance.players[playerIndex].indexShirt]);
        //newPlayer.transform.position = new Vector3(4.62f, 1.37f, 3.21f);
        players.Add(newPlayer);

        FixCinemachineCam(ref newPlayer);

        newPlayer.PreInitialize();
        newPlayer.Initialize();

        currentBeardIndex++;
        currentShirtIndex++;
    }

    private void FixCinemachineCam(ref Player player)
    {
        int layerToAdd = (int)Mathf.Log(playerCamMasks[players.Count - 1].value, 2);
        player.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<CinemachineBrain>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
    }
}
