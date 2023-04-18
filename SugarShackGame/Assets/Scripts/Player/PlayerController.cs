using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IFlow
{
    public PlayerStats stats;

    [HideInInspector] public float playerSpeed;
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool isRunning = false;

    private Vector2 look;
    private float lookRotation;

    [SerializeField] private GameObject camHolder;

    private void Start() {
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumped = context.action.triggered;
    }

    public void OnLook(InputAction.CallbackContext context) {
        look = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context) {
        if (context.action.triggered)
            isRunning = true;
    }

    public void OnTest(InputAction.CallbackContext context) {
        Debug.Log("Test called on " + gameObject.name + " !");
    }

    void Movement() {
        if (movementInput != Vector2.zero)
            playerSpeed = Mathf.MoveTowards(playerSpeed, isRunning ? stats.maxRunSpeed : stats.maxWalkSpeed, stats.accelerationSpeed * Time.deltaTime);
        else {
            playerSpeed = stats.initialSpeed;
            isRunning = false;
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.fixedDeltaTime * playerSpeed);


        // Changes the height position of the player..
        if (jumped && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(stats.jumpHeight * -3.0f * stats.gravityValue);
        }

        playerVelocity.y += stats.gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }
    private void Look() {
        // Turn
        transform.Rotate(Vector3.up * look.x * stats.sensitivity);

        // Look
        lookRotation += (-look.y * stats.sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -75, 75);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    public void PreInitialize() {
    }

    public void Initialize() {
        Cursor.lockState = CursorLockMode.Locked;

        controller = gameObject.GetComponent<CharacterController>();

        playerSpeed = stats.initialSpeed;
    }

    public void Refresh() {
    }

    public void PhysicsRefresh() {
        Movement();
        Look();
    }
}