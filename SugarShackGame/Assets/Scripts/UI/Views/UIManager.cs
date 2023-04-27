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

But be aware of this pitfall
*/

