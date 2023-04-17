using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IFlow
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float initialSpeed = 2.0f;
    [SerializeField] private float maxWalkSpeed = 5.0f;
    [SerializeField] private float maxRunSpeed = 10.0f;
    [SerializeField] private float accelerationSpeed = 0.5f;

    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool isMoving = false;
    private bool jumped = false;
    private bool isRunning = false;

    [SerializeField] private float sensitivity = 3;
    private Vector2 look;
    private float lookRotation;

    [SerializeField] private GameObject camHolder;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        controller = gameObject.GetComponent<CharacterController>();

        playerSpeed = initialSpeed;
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
        isMoving = true;
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumped = context.action.triggered;
    }

    public void OnLook(InputAction.CallbackContext context) {
        look = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.action.triggered)
            isRunning = true;
    }

    public void OnTest(InputAction.CallbackContext context) {
        Debug.Log("Test called on " + gameObject.name + " !");
    }

    private void FixedUpdate() {
        Movement();
        Look();
        isMoving = false;
    }

    void Movement() {
        if (movementInput != Vector2.zero)
            playerSpeed = Mathf.MoveTowards(playerSpeed, isRunning ? maxRunSpeed : maxWalkSpeed, accelerationSpeed * Time.deltaTime);
        else {
            playerSpeed = initialSpeed;
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
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }
    private void Look() {
        // Turn
        transform.Rotate(Vector3.up * look.x * sensitivity);

        // Look
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -75, 75);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    public void PreInitialize() {
    }

    public void Initialize() {
    }

    public void Refresh() {
    }

    public void PhysicsRefresh() {
        Movement();
    }
}