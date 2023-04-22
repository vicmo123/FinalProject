using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationEvents : MonoBehaviour
{
    public Action OnThrowAnimation = () => { };

    public void ThrowEvent()
    {
        if (OnThrowAnimation != null)
            OnThrowAnimation();
    }
}
