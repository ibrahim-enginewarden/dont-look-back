using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float moveSpeed = 4f;

    [Header("Look")]
    [SerializeField] private float MouseSensitivity = 0.15f;

    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float gravity = -9.81f;

    private CharacterController characterController;
    
    private float verticalRotation = 0f;

    private float verticalVelocity = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        HandleMovement();
        HandleLook();
    }
    private void HandleMovement()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            input = new Vector2(
                Keyboard.current.aKey.isPressed ? -1f :
                Keyboard.current.dKey.isPressed ? 1f : 0f,

                Keyboard.current.sKey.isPressed ? -1f :
                Keyboard.current.wKey.isPressed ? 1f : 0f
                );
        }

        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 movement =
            transform.right * input.x +
            transform.forward * input.y;

        if(characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        movement.y = verticalVelocity;

        characterController.Move(
            movement * moveSpeed * Time.deltaTime
            );
    }
    private void HandleLook()
    {
        if(Mouse.current == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * MouseSensitivity;
        float mouseY = mouseDelta.y * MouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

        cameraTransform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f); 
    }
}
