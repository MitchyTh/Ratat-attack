using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class RatMovement : MonoBehaviour
{
    [Header("References")]
    public Transform cameraHolder;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    [Header("Look")]
    public float mouseSensitivity = 0.1f;
    public float minLookAngle = -80f;
    public float maxLookAngle = 80f;

    private CharacterController controller;

    private Vector2 movementInput;
    private Vector2 lookInput;

    private float verticalVelocity;
    private float xRotation;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);

        if (cameraHolder != null)
        {
            cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private void HandleMovement()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
        move = transform.TransformDirection(move);
        move.y = verticalVelocity;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}