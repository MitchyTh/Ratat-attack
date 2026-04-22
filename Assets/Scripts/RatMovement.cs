using Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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

    public GameObject eatSenseBox;
    private EatScript eatScript;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    public TrashScript trashScript;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        eatScript = eatSenseBox.GetComponent<EatScript>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
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

    public void OnEat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (eatScript.canEatThis)
            {
                eatScript.foodScript.getEaten();
            }
        }
    }
    public void OnTipTrash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (eatScript.canTipTrash && trashScript.notTrash)
            {
                trashScript.notTrash = false;
                trashScript.SpawnTrash();
            }
        }
    }


    private void Update()
    {
        if (!controller.enabled) return;

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
    private void OnGrab(SelectEnterEventArgs args)
    {
        controller.enabled = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Reset Y and Z rotation to 0, keep X
        Vector3 rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rot.x, 0f, 0f);

        controller.enabled = true;
    }
}