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

    PlayerInputManager playerInputManager;
    PlayerFactory factory;
    public List<Player> players { get; private set; }

    //Temp for demo
    int currentBeardIndex = 0;
    int currentShirtIndex = 0;

    public void PreInitialize()
    {
        players = new List<Player>();

        playerInputManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInputManager>();
        playerInputManager.playerJoinedEvent.AddListener((val) => {
            Player newPlayer = new Player();

            val.gameObject.transform.position = Vector3.zero;
            newPlayer = val.gameObject.GetComponent<Player>();
            factory.ChangePlayerColor(ref newPlayer, factory.beardColors[currentBeardIndex], factory.shirtColors[currentShirtIndex]);
            
            newPlayer.PreInitialize();

            newPlayer.Initialize();

            players.Add(newPlayer);
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
            player.Refresh();
        }
    }
}
