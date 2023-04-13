using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IFlow
{

    public void PreInitialize()
    {
        AbilityManager.Instance().PreInitialize();
    }
    public void Initialize()
    {
        AbilityManager.Instance().Initialize();
    }

    public void PhysicsRefresh()
    {
        AbilityManager.Instance().PhysicsRefresh();
    }

    public void Refresh()
    {
        AbilityManager.Instance().Refresh();
    }


}
