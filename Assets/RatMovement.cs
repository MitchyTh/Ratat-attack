using UnityEngine;
using UnityEngine.InputSystem;

public class RatMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;
    private Vector2 movementInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // This gets called automatically by PlayerInput
    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        // Move relative to player facing direction
        move = transform.TransformDirection(move);

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
