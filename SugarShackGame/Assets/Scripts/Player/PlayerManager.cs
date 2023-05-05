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
    public GameObject[] spawnPositions;
    PlayerInputManager playerInputManager;
    PlayerFactory factory;
    public List<Player> players { get; private set; }
    private bool DEBUG = false;
    public bool playersJoined = false;


    //Temp for demo
    int currentBeardIndex = 0;
    int currentShirtIndex = 0;

    public void PreInitialize()
    {
        if (DEBUG)
        {
            Debug.Log("PlayerManager PreInitialize");
        }
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
        if (DEBUG)
        {
            Debug.Log("PlayerManager Initialize");
        }
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPoint");
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
        if (DEBUG)
        {
            Debug.Log("PlayerManager Initialize Player");
            Debug.Log("player" + input.user.index + " using : " + input.user.pairedDevices[0]);
        }
        Player generatedPlayer = input.gameObject.GetComponent<Player>();
        Debug.Log($"Index barbe joueur { players.Count}: " + UIManager.Instance.playersGD[players.Count].indexBeard);
        Debug.Log("Index barbe joueur { players.Count} : " + UIManager.Instance.playersGD[players.Count].indexShirt);
        factory.ChangePlayerColor(ref generatedPlayer, factory.beardColors[UIManager.Instance.playersGD[players.Count].indexBeard], factory.shirtColors[UIManager.Instance.playersGD[players.Count].indexShirt]);
        players.Add(generatedPlayer);
        //Assign index to the player
        players[players.Count - 1].index = players.Count - 1;

        FixCinemachineCam(players[players.Count - 1]);
        players[players.Count - 1].color = UIManager.Instance.AssignColor(factory.shirtColors[UIManager.Instance.playersGD[players.Count - 1].indexShirt]);

        //Debug.Log(factory.shirtColors[UIManager.Instance.playersGD[players.Count -1].indexShirt].ToString());

        players[players.Count - 1].PreInitialize();
        players[players.Count - 1].Initialize();
        players[players.Count - 1].SpawnAtLocation(spawnPositions[players.Count - 1].transform.position);

        currentBeardIndex++;
        currentShirtIndex++;

        playersJoined = true;
    }

    private void FixCinemachineCam(Player player)
    {
        int layerToAdd = (int)Mathf.Log(playerCamMasks[players.Count - 1].value, 2);
        player.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<CinemachineBrain>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
    }
}
