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

        playerInputManager = PlayerConfigurationManager.Instance.GetComponent<PlayerInputManager>();
       
        //playerInputManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputManager>();
        //playerInputManager.onPlayerJoined += (input) =>
        //{
        //    InitializePlayer(input);
        //};
        factory = new PlayerFactory("Prefabs/Player/Player");
    }

    public void Initialize()
    {
        if (DEBUG)
        {
            Debug.Log("PlayerManager Initialize");
        }
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPoint");
        playerInputManager.splitScreen = true;
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

    public Player InitializePlayer(PlayerConfiguration playerConfig)
    {

        CustomInputHandler customHandler = playerConfig.Input.gameObject.GetComponent<CustomInputHandler>();
        
        if (DEBUG)
        {
            Debug.Log("PlayerManager Initialize Player");
            Debug.Log("player" + playerConfig.PlayerIndex + " using : " + playerConfig.Input.currentControlScheme);
        }


        Debug.Log("Instantiating player");
        Player generatedPlayer = factory.CreatPlayer(factory.beardColors[playerConfig.IndexColorBeard], factory.shirtColors[playerConfig.IndexColorShirt]);
        generatedPlayer.GetComponent<PlayerController>().SetInputHandler(customHandler);
        generatedPlayer.SetPlayerInput(playerConfig.Input);
     
        players.Add(generatedPlayer);
        //Assign index to the player
        players[players.Count - 1].index = playerConfig.PlayerIndex;
        FixCinemachineCam(players[players.Count - 1]);

        //Temp store the values of Input
        int index = playerConfig.PlayerIndex;
        string controlscheme = playerConfig.Input.currentControlScheme;
        PlayerInput plInput = generatedPlayer.GetComponent<PlayerInput>();
                
      
        players[players.Count - 1].color = UIManager.Instance.AssignColor(factory.shirtColors[playerConfig.IndexColorShirt]);
        players[players.Count - 1].PreInitialize();
        players[players.Count - 1].Initialize();
        players[players.Count - 1].SpawnAtLocation(spawnPositions[players.Count - 1].transform.position, spawnPositions[players.Count - 1].transform.rotation);
        
        playersJoined = true;

        return generatedPlayer;
    }

    private void FixCinemachineCam(Player player)
    {
        int layerToAdd = (int)Mathf.Log(playerCamMasks[players.Count - 1].value, 2);
        player.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<CinemachineBrain>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
    }
}
