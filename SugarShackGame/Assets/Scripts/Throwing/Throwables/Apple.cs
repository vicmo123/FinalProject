using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Throwable
{
    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("snowBall init");
    }
}