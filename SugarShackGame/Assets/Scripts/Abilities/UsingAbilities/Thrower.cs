using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Thrower : MonoBehaviour, IFlow
{
    [Header("Throwing Settings")]
    public Transform attachPoint;
    private CustomInputHandler inputHandler;
    [SerializeField]
    private Camera cam;
    private float timeHeld = 0f;

    [HideInInspector]
    public bool IsHoldingThrowable = false;
    [HideInInspector]
    public ThrowableAbility toThrow = null;

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
            timeHeld += Time.deltaTime;

            animator.SetBool(_TimeToThrowId, false);
            animator.SetBool(_HoldingThrowableId, IsHoldingThrowable);
        }

        if (!inputHandler.Throw)
        {
            animator.SetBool(_TimeToThrowId, true);
            animator.SetBool(_HoldingThrowableId, IsHoldingThrowable);
        }
    }

    public void PhysicsRefresh()
    {

    }

    public void OnThrowLogic()
    {
        toThrow.Throw(timeHeld);
        toThrow = null;
        IsHoldingThrowable = false;
        timeHeld = 0;
    }
}
