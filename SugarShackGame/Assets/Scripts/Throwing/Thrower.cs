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
    [SerializeField]
    private RectTransform cursor;
    [SerializeField]
    private float cursorClipPlane;

    [SerializeField]
    private float speed = 10f;

    [HideInInspector]
    public bool IsHoldingThrowable = false;
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
                SoundManager.Play?.Invoke(SoundListEnum.Bell);
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
            SoundManager.Play?.Invoke(SoundListEnum.Chop);
            //ThrowableManager.Instance.AddObjectToCollection(ThrowableTypes.Apple);
        }

        if (inputHandler.UseRightPowerUp)
        {
            SoundManager.Play?.Invoke(SoundListEnum.completetask_0);
            //ThrowableManager.Instance.AddObjectToCollection(ThrowableTypes.Trampoline);
        }
    }

    public void PhysicsRefresh()
    {

    }

    public void OnThrowLogic()
    {
        float timeHeld = inputHandler.ThrowForce;
        toThrow.Throw(ComputeVelocityTowardsCursor() * speed * timeHeld);
        toThrow = null;
        IsHoldingThrowable = false;
        //Debug.Log("Throw");
    }

    private Vector3 ComputeVelocityTowardsCursor()
    {
        // Get the position of the cursor in the viewport
        Vector3 cursorPosViewport = new Vector3(0.5f, 0.5f, cam.farClipPlane / 2f);

        // Get the direction from the player to the cursor position
        Vector3 throwDirection = cam.ViewportToWorldPoint(cursorPosViewport) - attachPoint.position;
        //Debug.Log("throwDirection: " + throwDirection); // Debugging line

        // Normalize the direction and multiply by the throw force to get the throw velocity
        Vector3 throwVelocity = throwDirection.normalized;

        return throwVelocity;
    }
}
