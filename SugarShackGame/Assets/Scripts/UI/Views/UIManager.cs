using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Gather all the different scenes
    //Plays a song before game starts
    //Remembers information gathered in UI from player

    public void LoadScenes()
    {
        Debug.Log("Loading scenes");
    }
}
