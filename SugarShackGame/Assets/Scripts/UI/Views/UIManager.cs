using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public enum ScenesNames { MainMenu, NewSelectionMenu, Title, NewGamePlay, EndGame, Controls, Credits };

public class UIManager
{
    #region Singleton
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    private UIManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    private bool DEBUG_MODE = false;
    public float gameDuration = 300f;
    private List<string> scenes;
    private int currentScene = 0;
    private List<ScenesNames> sceneNames;
    private PlayerScore[] playerScores;

    public int CurrentScene { get => currentScene; set => currentScene = value; }

    public PlayerGameData[] playersGD;


    public void Initialize()
    {
        playersGD = new PlayerGameData[2] { new PlayerGameData(), new PlayerGameData() };

        if (DEBUG_MODE)
        {
            FakeFillPlayerGameData();
        }
        sceneNames = System.Enum.GetValues(typeof(ScenesNames)).Cast<ScenesNames>().ToList();
        CurrentScene = 0;
    }
    private void FakeFillPlayerGameData()
    {
        for (int i = 0; i < playersGD.Length; i++)
        {
            playersGD[i].connected = true;
            playersGD[i].playerIndex = i + 1;
            playersGD[i].indexShirt = i + 1;
            playersGD[i].indexBeard = i;
            playersGD[i].deviceId = i;
            playersGD[i].name = $"player{i + 1}";
        }
    }
    public Color StringToColor(string color)
    {
        Color newColor = Color.yellow;
        switch (color)
        {
            case "Blue":
                newColor = Color.blue;
                break;
            case "Green":
                newColor = Color.green;
                break;
            case "Pink":
                newColor = Color.magenta;
                break;
            case "Red":
                newColor = Color.red;
                break;
            case "Yellow":
                newColor = Color.yellow;
                break;

        }
        return newColor;
    }

    public void LoadOneScene(ScenesNames name)
    {
        SceneManager.LoadScene(name.ToString());
    }

    public void LoadNextScene()
    {
        CurrentScene++;
        SceneManager.LoadScene(CurrentScene);
    }

    public void LoadPreviousScene()
    {
        if (CurrentScene != 0)
        {
            --CurrentScene;
            SceneManager.LoadScene(CurrentScene);
        }
        else
        {
            throw new System.Exception("You can't go back to previous scene.");
        }
    }
    
    public PlayerGameData[] GetPlayerInfo()
    {
        return playersGD;
    }


    public void ClearData()
    {
        for (int i = 0; i < playersGD.Length; i++)
        {
            playersGD[i] = null;
        }
        playersGD = null;
    }
    public void InitializeGameData()
    {
        Debug.Log("Initializing Game Data in UIManager");
    }

    public void SaveScore(PlayerScore[] playerScores)
    {
        this.playerScores = playerScores;
    }

    public PlayerScore[] GetScores()
    {
        return this.playerScores;
    }
   
}

public class PlayerGameData
{
    public int playerIndex = 0;
    public string name = "";
    public string deviceName = "";
    public int deviceId = 0;
    public int indexBeard = 0;
    public int indexShirt = 0;
    public bool connected = false;


    public int GetId()
    {
        return playerIndex;
    } 
}