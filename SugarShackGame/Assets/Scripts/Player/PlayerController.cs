using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerController : MonoBehaviour, IFlow
{
    //For better movement purposes
    [Tooltip("Main camera for this player's instance")]
    [SerializeField] private GameObject _mainCamera;
    [Tooltip("Object that rotates inside the prefab, to be able to rotate camera around the player")]
    [SerializeField] private Transform CinemachineCameraRotationTarget;
    [Tooltip("The model object of the prefab so you can move it with without causing problems")]
    [SerializeField] private Transform model;
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField]
    private GameObject CinemachineCameraTarget;

    //Stats for movement control
    [SerializeField] private PlayerStats playerStats;

    //Inputs
    [SerializeField]
    private CustomInputHandler _inputHandler;

    //Cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    //Player
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private bool _grounded = true;
    private int _numberJumps = 0;

    //Timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    //Animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CharacterController _controller;

    

    private const float _threshold = 0.01f;

    private bool _hasAnimator;

    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }

    public void PreInitialize()
    {
        _grounded = playerStats.GroundedInitialValue;
        if (_animator)
        {
            _hasAnimator = true;
        }
    }

    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;

         _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
        if (_hasAnimator)
        {
            AssignAnimationIDs();
        }
        
        // reset our timeouts on start
        _jumpTimeoutDelta = playerStats.JumpTimeout;
        _fallTimeoutDelta = playerStats.FallTimeout;
    }

    public void Refresh()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
        RotateCamTarget();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    public void PhysicsRefresh()
    {
        
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - playerStats.GroundedOffset,
            transform.position.z);
        _grounded = Physics.CheckSphere(spherePosition, playerStats.GroundedRadius, playerStats.GroundLayers,
            QueryTriggerInteraction.Ignore);

        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDGrounded, _grounded);
        }

        if (_grounded)
        {
            //Debug.Log("num" + _numberJumps);
            _numberJumps = 0;
        }
    }

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (_inputHandler.move.sqrMagnitude >= _threshold && !playerStats.LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += _inputHandler.move.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _inputHandler.move.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, playerStats.BottomClamp, playerStats.TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + playerStats.CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private void RotateCamTarget()
    {
        // normalise input direction
        Vector3 inputDirection = new Vector3(_inputHandler.look.x, 0.0f, _inputHandler.look.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_inputHandler.look != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(CinemachineCameraRotationTarget.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                playerStats.RotationSmoothTime);

            // rotate to face input direction relative to camera position
            CinemachineCameraRotationTarget.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }

    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _inputHandler.sprint ? playerStats.SprintSpeed : playerStats.MoveSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_inputHandler.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = playerStats.analogMovement ? _inputHandler.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * playerStats.SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * playerStats.SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_inputHandler.move.x, 0.0f, _inputHandler.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_inputHandler.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(model.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                playerStats.RotationSmoothTime);

            // rotate to face input direction relative to camera position
            model.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }

    private void JumpAndGravity()
    {
        if (_grounded)
        {
            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            //Jump
            if (_inputHandler.jump && _jumpTimeoutDelta <= 0.0f && _numberJumps < 2)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(playerStats.JumpHeight * -2f * playerStats.Gravity);

                //// update animator if using character
                //if (_hasAnimator)
                //{
                //    _animator.SetBool(_animIDJump, true);
                //}
                _numberJumps++;
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }

            _inputHandler.jump = false;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += playerStats.Gravity * 3 * Time.deltaTime;
        }
    }

    //private void JumpAndGravity()
    //{
    //    if (Grounded)
    //    {
    //        // reset the fall timeout timer
    //        _fallTimeoutDelta = FallTimeout;

    //        // update animator if using character
    //        if (_hasAnimator)
    //        {
    //            _animator.SetBool(_animIDJump, false);
    //            _animator.SetBool(_animIDFreeFall, false);
    //        }

    //        // stop our velocity dropping infinitely when grounded
    //        if (_verticalVelocity < 0.0f)
    //        {
    //            _verticalVelocity = -2f;
    //        }

    //        // Jump
    //        if (jump && _jumpTimeoutDelta <= 0.0f)
    //        {
    //            // the square root of H * -2 * G = how much velocity needed to reach desired height
    //            _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

    //            // update animator if using character
    //            if (_hasAnimator)
    //            {
    //                _animator.SetBool(_animIDJump, true);
    //            }
    //        }

    //        // jump timeout
    //        if (_jumpTimeoutDelta >= 0.0f)
    //        {
    //            _jumpTimeoutDelta -= Time.deltaTime;
    //        }
    //    }
    //    else
    //    {
    //        // reset the jump timeout timer
    //        _jumpTimeoutDelta = JumpTimeout;

    //        // fall timeout
    //        if (_fallTimeoutDelta >= 0.0f)
    //        {
    //            _fallTimeoutDelta -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            // update animator if using character
    //            if (_hasAnimator)
    //            {
    //                _animator.SetBool(_animIDFreeFall, true);
    //            }
    //        }

    //        // if we are not grounded, do not jump
    //        jump = false;
    //    }

    //    // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
    //    if (_verticalVelocity < _terminalVelocity)
    //    {
    //        _verticalVelocity += Gravity * Time.deltaTime;
    //    }
    //}

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - playerStats.GroundedOffset, transform.position.z),
            playerStats.GroundedRadius);
    }

    //private void OnFootstep(AnimationEvent animationEvent)
    //{
    //    if (animationEvent.animatorClipInfo.weight > 0.5f)
    //    {
    //        if (FootstepAudioClips.Length > 0)
    //        {
    //            var index = Random.Range(0, FootstepAudioClips.Length);
    //            AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
    //        }
    //    }
    //}

    //private void OnLand(AnimationEvent animationEvent)
    //{
    //    if (animationEvent.animatorClipInfo.weight > 0.5f)
    //    {
    //        AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
    //    }
    //}
}
