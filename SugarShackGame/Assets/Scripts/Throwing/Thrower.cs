using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour, IFlow
{
    public Transform attachPoint;
    public CustomInputHandler inputHandler;

    public float throwForce;
    public Vector3 velocity;
    public float coolDown;

    public bool IsHoldingThrowable = false;
    public Throwable toThrow = null;

    public void PreInitialize()
    {

    }

    public void Initialize()
    {

    }

    public void Refresh()
    {
        if (inputHandler.ThrowPressed)
        {
            if (!IsHoldingThrowable)
            {
                toThrow = ThrowableManager.Instance.AddObjectToCollection(ThrowableTypes.SnowBall);
                toThrow.AttachToThrower(this);
                IsHoldingThrowable = true;
            }
        }

        if (inputHandler.ThrowReleased)
        {
            if (IsHoldingThrowable)
            {
                float timeHeld = inputHandler.ThrowForce;
                Debug.Log(timeHeld);
                toThrow.Throw(Vector3.zero);
                toThrow = null;
                IsHoldingThrowable = false;
            }
        }

        if (inputHandler.UseLeftPowerUp)
        {
            ThrowableManager.Instance.AddObjectToCollection(ThrowableTypes.Apple);
        }

        if (inputHandler.UseRightPowerUp)
        {
            ThrowableManager.Instance.AddObjectToCollection(ThrowableTypes.Trampoline);
        }
    }

    public void PhysicsRefresh()
    {

    }
}
