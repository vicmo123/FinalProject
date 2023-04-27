using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public enum ScenesNames { MainMenu, Setup, Title, GamePlay, EndGame};

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
    private int currentSceneIndex;
    private List<ScenesNames> sceneNames;
    //Gather all the different scenes
    //Plays a song before game starts
    //Remembers information gathered in UI from player

    public void LoadScenes()
    {
        sceneNames = System.Enum.GetValues(typeof(ScenesNames)).Cast<ScenesNames>().ToList();
        currentSceneIndex = 0;
    }   

    public void LoadOneScene(ScenesNames name)
    {
        SceneManager.LoadScene(name.ToString());
    }

    public void LoadNextScene()
    {
        ++currentSceneIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadPreviousScene()
    {
        --currentSceneIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
