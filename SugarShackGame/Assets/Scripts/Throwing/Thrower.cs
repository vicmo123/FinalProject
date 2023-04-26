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
    private float speed = 10f;
    [SerializeField]
    private float maxSpeed = 50f;
    private float timeHeld = 0f;

    [Header("Aiming")]
    [Header("Display Controls")]
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    [Range(10, 100)]
    private int linePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float timeBetweenPoints = 0.1f;
    [SerializeField]
    private Transform launchPosition;
    [SerializeField]
    private LayerMask throwableCollisionMask;

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
            timeHeld += Time.deltaTime;
            
            animator.SetBool(_TimeToThrowId, false);
            animator.SetBool(_HoldingThrowableId, inputHandler.Throw);

            if (!IsHoldingThrowable)
            {
                SoundManager.Play?.Invoke(SoundListEnum.Bell);
                ThrowableManager.Instance.TryAddObjectToCollection(ThrowableTypes.SnowBall, this);
            }

            DrawProjection();
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
        toThrow.Throw(gameObject.transform.forward * ComputeSpeed());
        toThrow = null;
        IsHoldingThrowable = false;
        timeHeld = 0;
        lineRenderer.enabled = false;
    }

    //private Vector3 ComputeVelocityTowardsCursor()
    //{
    //    // Get the position of the cursor in the viewport
    //    Vector3 cursorPosViewport = new Vector3(0.5f, 0.5f, cam.farClipPlane / 2f);

    //    // Get the direction from the player to the cursor position
    //    Vector3 throwDirection = cam.ViewportToWorldPoint(cursorPosViewport) - attachPoint.position;
    //    //Debug.Log("throwDirection: " + throwDirection); // Debugging line

    //    // Normalize the direction and multiply by the throw force to get the throw velocity
    //    Vector3 throwVelocity = throwDirection.normalized;

    //    return throwVelocity;
    //}

    private float ComputeSpeed()
    {
        return Mathf.Clamp(speed * timeHeld, speed, maxSpeed);
    }

    public void ComputeNewLayerMask()
    {
        int ballLAyer = toThrow.gameObject.layer;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(ballLAyer, i))
            {
                throwableCollisionMask |= 1 << i; // magic
            }
        }
    }

    private void DrawProjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
        Vector3 startPosition = launchPosition.position;
        Vector3 startVelocity = ComputeSpeed() * gameObject.transform.forward / toThrow.rb.mass;

        int i = 0;
        lineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);

            Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition,
                (point - lastPosition).normalized,
                out RaycastHit hit,
                (point - lastPosition).magnitude,
                throwableCollisionMask))
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                return;
            }
        }
    }
}
