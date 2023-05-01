using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationEvents : MonoBehaviour
{
    public Action OnThrowAnimation = () => { };

    public Action OnRightStep = () => { };

    public Action OnLeftStep = () => { };

    public void ThrowEvent()
    {
        if (OnThrowAnimation != null)
            OnThrowAnimation();
    }

    public void RightStepEvent()
    {
        if (OnRightStep != null)
            OnRightStep();
    }

    public void LeftStepEvent()
    {
        if (OnLeftStep != null)
            OnLeftStep();
    }
}
