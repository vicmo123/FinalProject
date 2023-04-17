using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : IFlow
{
    #region Singleton
    private static UIGameManager instance;

    public static UIGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIGameManager();
            }
            return instance;
        }
    }

    private UIGameManager
()
    {
    }
    #endregion

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }

    public void PreInitialize()
    {
    }

    public void Refresh()
    {
    }
}