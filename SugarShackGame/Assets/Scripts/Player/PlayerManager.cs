using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    PlayerFactory factory;
    Player player1;
    Player player2;

    //Temp for demo
    int currentBeardIndex = 0;
    int currentShirtIndex = 0;

    public void PreInitialize()
    {
        factory = new PlayerFactory();
    }

    public void Initialize()
    {
        AddPlayers();
    }

    public void Refresh()
    {
        player1.Refresh();
        //player2.Refresh();

        //For demo
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentBeardIndex++;
            if (currentBeardIndex >= factory.beardColorList.Count)
            {
                currentBeardIndex = 0;
            }

            factory.ChangePlayerColor(ref player1, factory.beardColorList[currentBeardIndex], factory.shirtColorList[currentShirtIndex]);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            currentShirtIndex++;
            if (currentShirtIndex >= factory.shirtColorList.Count)
            {
                currentShirtIndex = 0;
            }

            factory.ChangePlayerColor(ref player1, factory.beardColorList[currentBeardIndex], factory.shirtColorList[currentShirtIndex]);
        }
    }

    public void PhysicsRefresh()
    {
        player1.PhysicsRefresh();
        //player2.PhysicsRefresh();
    }

    public void AddPlayers()
    {
        player1 = factory.CreatPlayer(factory.beardColorList[currentBeardIndex], factory.shirtColorList[currentShirtIndex]);
        //player2 = factory.CreatPlayer(factory.beardColorList[1], factory.shirtColorList[0]);

        player1.PreInitialize();
        //player2.PreInitialize();

        player1.Initialize();
        //player2.Initialize();
    }
}
