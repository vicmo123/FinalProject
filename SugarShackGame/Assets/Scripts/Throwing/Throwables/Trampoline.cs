using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Throwable
{
    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("Trampoline init");
    }
}
