using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAbility : Ability
{
    public override void Activate()
    {
        base.Activate();
        Debug.Log("ThrowableAbility activation");
        //ThrowableManager.Instance.AddObjectToCollection()
    }
}
