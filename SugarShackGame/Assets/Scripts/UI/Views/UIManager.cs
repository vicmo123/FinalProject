using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public enum ScenesNames { MainMenu, Setup, Title, GamePlay, EndGame, Credits, Controls };

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
    public float gameDuration = 500f;
    private List<string> scenes;
    private int currentScene = 0;
    private List<ScenesNames> sceneNames;

    public int CurrentScene { get => currentScene; set => currentScene = value; }

    public PlayerGameData[] playersGD;


    public void Initialize()
    {
        Debug.Log("Initialise UIManage");
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
    public Color AssignColor(string color)
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
        Debug.Log("Current scene : " + CurrentScene);
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

    public void GatherData()
    {
        Debug.Log("Gathering Data for the end of the game");
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

/***
 * Advice / warning from Matthew
 * If you wanted a state machine that can always run and run code between scenes, it defiantly needs to be a singleton which can persist across scenes.

It could then have high level control over being able to run state logic and call scene change code.

Be aware that scenes do not load right away.

What I mean is code like this:

 

 

//do stuff

SceneManager.LoadScene("KitchenScene"); //Change scene

Soup bowlOfSoupInScene = GameObject.FindObjectOfType<Soup>(); //This line can sometimes fail, sometimes not.

 

So when you change scene, it is done async (using a courtine), so the main thread continues and calls the function to search the scene for a Soup component.

If your computer is very fast, it may load the scene before the next line.

If not, then it will crash or find soup on the original scene instead of the next scene because it ran before the scene loaded.

 

The solution is simple, scene manager contains an event system you can listen for when scene finishes loading.

But be aware of this pitfall : !!!!! = > Event called : sceneLoaded
*/

