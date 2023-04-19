using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Manager(typeof(SnowBallManager))]
public class SnowBallManager : IFlow
{
    #region Singleton
    private static SnowBallManager instance;

    public static SnowBallManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SnowBallManager();
            }
            return instance;
        }
    }

    private SnowBallManager()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    public GenericWrapper<SnowBallTypes, SnowBall, SnowBallData> snowBallSystem;
    private CustomInputHandler _inputHandler;
    [SerializeField] private int prefillAmountPerType = 5;

    public void PreInitialize()
    {
        snowBallSystem = GenericWrapper<SnowBallTypes, SnowBall, SnowBallData>.Instance;
        snowBallSystem.PreInitialize();
    }

    public void Initialize()
    {
        snowBallSystem.Initialize();
    }

    public void Refresh()
    {
        snowBallSystem.Refresh();
    }

    public void PhysicsRefresh()
    {
        snowBallSystem.PhysicsRefresh();
    }
}
