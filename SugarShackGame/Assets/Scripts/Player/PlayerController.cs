using Cinemachine;
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
    //Camera stuff
    [Header("Camera")]
    [Tooltip("Main camera for this player's instance")]
    [SerializeField] private GameObject _mainCamera;
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField]
    private GameObject _cinemachineCameraTarget;
    [Tooltip("To be able to move the camera when aiming")]
    [SerializeField]
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField]
    private GameObject cursor;

    //Stats for movement control
    [Header("Player Stats")]
    [SerializeField] private PlayerStats _playerStats;

    //Inputs
    private CustomInputHandler _inputHandler;
#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif

    //Other
    private Animator _animator;
    private CharacterController _controller;
    private Thrower _thrower;

    //Cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private Cinemachine3rdPersonFollow _followComponent;

    //Player
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private bool _grounded = true;

    //Timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    //Animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    //Animation variables
    private const float _threshold = 0.01f;
    private bool _hasAnimator;

    private bool IsCurrentDeviceMouse {
        get {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "Keyboard";
#else
				return false;
#endif
        }
    }

    public void PreInitialize() {
        Cursor.lockState = CursorLockMode.Locked;

        _cinemachineTargetYaw = _cinemachineCameraTarget.transform.rotation.eulerAngles.y;
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
        _controller = GetComponent<CharacterController>();
        _inputHandler = GetComponent<CustomInputHandler>();
        _animator = GetComponent<Animator>();
        _followComponent = _cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _followComponent.CameraSide = _playerStats.initialCameraSideValue;
        _followComponent.CameraDistance = _playerStats.initialCameraDistanceValue;
        _thrower = GetComponent<Thrower>();

        if (_animator) {
            _hasAnimator = true;
        }

        _grounded = _playerStats.GroundedInitialValue;
    }

    public void Initialize() {

        if (_hasAnimator) {
            AssignAnimationIDs();
        }

        // reset our timeouts on start
        _jumpTimeoutDelta = _playerStats.JumpTimeout;
        _fallTimeoutDelta = _playerStats.FallTimeout;
    }

    public void Refresh() {
        Aim();
        JumpAndGravity();
        GroundedCheck();
        Move();
        //CheckForUsing();
    }

    private void LateUpdate() {
        if(_inputHandler && _playerStats)
            CameraRotation();
    }

    public void PhysicsRefresh() {

    }

    private void AssignAnimationIDs() {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void GroundedCheck() {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _playerStats.GroundedOffset,
            transform.position.z);
        _grounded = Physics.CheckSphere(spherePosition, _playerStats.GroundedRadius, _playerStats.GroundLayers,
            QueryTriggerInteraction.Ignore);

        // update animator if using character
        if (_hasAnimator) {
            _animator.SetBool(_animIDGrounded, _grounded);
        }
    }

    private void CameraRotation() {
        // if there is an input and camera position is not fixed
        if (_inputHandler.look.sqrMagnitude >= _threshold && !_playerStats.LockCameraPosition) {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += _inputHandler.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _inputHandler.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _playerStats.BottomClamp, _playerStats.TopClamp);

        // Cinemachine will follow this target
        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _playerStats.CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private void Move() {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _inputHandler.sprint ? _playerStats.SprintSpeed : _playerStats.MoveSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_inputHandler.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _inputHandler.analogMovement ? _inputHandler.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset) {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * _playerStats.SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _playerStats.SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_inputHandler.move.x, 0.0f, _inputHandler.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_inputHandler.move != Vector2.zero) {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

            // rotate to face input direction relative to camera position
            if (!_inputHandler.Aim && !_inputHandler.Throw && !_thrower.IsHoldingThrowable) {
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _playerStats.RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        // update animator if using character
        if (_hasAnimator) {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }

    private void JumpAndGravity() {
        if (_grounded) {
            // reset the fall timeout timer
            _fallTimeoutDelta = _playerStats.FallTimeout;

            // update animator if using character
            if (_hasAnimator) {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
            }

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f) {
                _verticalVelocity = -2f;
            }

            // Jump
            if (_inputHandler.jump && _jumpTimeoutDelta <= 0.0f) {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(_playerStats.JumpHeight * -2f * _playerStats.Gravity);

                // update animator if using character
                if (_hasAnimator) {
                    _animator.SetBool(_animIDJump, true);
                }
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f) {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else {
            // reset the jump timeout timer
            _jumpTimeoutDelta = _playerStats.JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f) {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else {
                // update animator if using character
                if (_hasAnimator) {
                    _animator.SetBool(_animIDFreeFall, true);
                }
            }

            // if we are not grounded, do not jump
            _inputHandler.jump = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity) {
            _verticalVelocity += _playerStats.Gravity * Time.deltaTime;
        }
    }

    public void Aim() {
        if (_inputHandler.Aim || _inputHandler.Throw) {
            if (_inputHandler.Aim) {
                if (_followComponent.CameraSide < _playerStats.initialCameraSideValue) {
                    _followComponent.CameraSide += _playerStats.aimSpeed * Time.deltaTime;
                }

                if (_followComponent.CameraDistance > _playerStats.TargetCameraDistanceValue) {
                    _followComponent.CameraDistance -= _playerStats.aimSpeed * _playerStats.CameraDistanceMultiplicator * Time.deltaTime;
                }
            }

            if (!cursor.gameObject.activeInHierarchy) {
                cursor.gameObject.SetActive(true);
            }

            _targetRotation = Mathf.Atan2(_mainCamera.transform.forward.x, _mainCamera.transform.forward.z) * Mathf.Rad2Deg;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _playerStats.RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        }

        if (!_inputHandler.Aim) {
            if (_followComponent.CameraSide > _playerStats.initialCameraSideValue) {
                _followComponent.CameraSide -= _playerStats.aimSpeed * Time.deltaTime;
            }

            if (_followComponent.CameraDistance < _playerStats.initialCameraDistanceValue) {
                _followComponent.CameraDistance += _playerStats.aimSpeed * _playerStats.CameraDistanceMultiplicator * Time.deltaTime;
            }

            if (cursor.gameObject.activeInHierarchy) {
                cursor.gameObject.SetActive(false);
            }
        }
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax) {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnDrawGizmosSelected() {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - _playerStats.GroundedOffset, transform.position.z),
            _playerStats.GroundedRadius);
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

    //private void CheckForUsing() {
    //    Player _player = transform.GetComponent<Player>();

    //    RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, .3f, 0), 1, transform.TransformDirection(Vector3.forward), 1000);

    //    if (hits.Length > 0) {
    //        foreach (var hit in hits) {
    //            if (hit.transform.gameObject.CompareTag("Bucket")) {
    //                Bucket bucket = hit.transform.GetComponent<Bucket>();

    //                if (_inputHandler.Use)
    //                    bucket.Use(_player);
    //            }

    //            if (hit.transform.gameObject.CompareTag("Cauldron")) {
    //                Cauldron cauldron = hit.transform.GetComponent<Cauldron>();

    //                if (cauldron.player == _player) {
    //                    if (_inputHandler.Use)
    //                        cauldron.Use(_player);
    //                }
    //            }
    //        }
    //    }
    //    //Debug.DrawRay(model.position + new Vector3(0, .5f, 0), model.TransformDirection(Vector3.forward), Color.red, 1000);
    //}
}
