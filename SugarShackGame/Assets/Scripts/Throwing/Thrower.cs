using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour, IFlow
{
    [Header("Throwing")]
    public Transform attachPoint;
    private CustomInputHandler inputHandler;

    public float throwForce;
    public Vector3 velocity;
    public float coolDown;

    private bool IsHoldingThrowable = false;
    [HideInInspector]
    public Throwable toThrow = null;

    private Animator animator;
    private int _HoldingThrowableId;
    private int _TimeToThrowId;

    private PlayerAnimationEvents animEventReciever;
    

    public void PreInitialize()
    {
        inputHandler = GetComponent<CustomInputHandler>();
        animator = GetComponent<Animator>();
        animEventReciever = GetComponent<PlayerAnimationEvents>();
        animEventReciever.OnThrowAnimation += () => { OnThrowLogic(); };
    }

    public void Initialize()
    {
        _HoldingThrowableId = Animator.StringToHash("IsHoldingThrowable");
        _TimeToThrowId = Animator.StringToHash("ThrowTime");
    }

    public void Refresh()
    {
        if (inputHandler.Throw)
        {
            animator.SetBool(_TimeToThrowId, false);
            animator.SetBool(_HoldingThrowableId, inputHandler.Throw);

            if (!IsHoldingThrowable)
            {
                toThrow = ThrowableManager.Instance.AddObjectToCollection(ThrowableTypes.SnowBall);
                toThrow.AttachToThrower(this);
                IsHoldingThrowable = true;
            }
        }

        if (!inputHandler.Throw)
        {
            animator.SetBool(_TimeToThrowId, true);
            animator.SetBool(_HoldingThrowableId, IsHoldingThrowable);
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

    public void OnThrowLogic()
    {
        float timeHeld = inputHandler.ThrowForce;
        toThrow.Throw(Vector3.forward * timeHeld);
        toThrow = null;
        IsHoldingThrowable = false;
        Debug.Log("Throw");
    }
}
