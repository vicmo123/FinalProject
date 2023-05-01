using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public enum ScenesNames { MainMenu, Setup, Title, GamePlay, EndGame };

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

    public float gameDuration;
    private List<string> scenes;
    private int currentScene = 0;
    private List<ScenesNames> sceneNames;

    public int CurrentScene { get => currentScene; set => currentScene = value; }

    public PlayerGameData[] players;

    #region Player1Data
    public string p1_name = "Player 1";
    public string p1_deviceName = "";
    public int p1_deviceId;
    #endregion

    #region Player2Data
    public string p2_name = "Player 2";
    public string p2_deviceName = "";
    public int p2_deviceId  ;
    #endregion

    public void Initialize()
    {
        players = new PlayerGameData[2] { new PlayerGameData(), new PlayerGameData() };

        sceneNames = System.Enum.GetValues(typeof(ScenesNames)).Cast<ScenesNames>().ToList();
        CurrentScene = 0;
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
        return players;
    }
    public void ClearData()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = null;
        }
        players = null;
    }

}

public class PlayerGameData
{
    public static int playerId = 0;
    public string name = "";
    public string deviceName = "";
    public int deviceId = 0;
    public int indexBeard= 0;
    public int indexShirt = 0;
    public bool connected = false;

    public PlayerGameData()
    {
        playerId++;
    }

    public int GetId()
    {
        return playerId;
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

