using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 12;
    public float jumpGravity = -20;
    public float fallGravity = -35;
    public float airSpeedMultiplier = 1.3f;
    public float minZ = -2.7f;
    public float maxZ = 2.7f;

    private CharacterController controller;
    private Vector2 movementInput;
    private Vector3 velocity;
    private bool jumpPressed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpPressed = true;
        }
    }

    public void Bounce(float force)
    {
        velocity.y = force;
    }

    void Update()
    {
        float currentSpeed = controller.isGrounded
            ? speed
            : speed * airSpeedMultiplier;

        Vector3 movement = new Vector3(
            movementInput.x,
            0,
            movementInput.y
        );

        movement = Vector3.ClampMagnitude(movement, 1);

        Vector3 displacement = movement * currentSpeed * Time.deltaTime;

        float targetZ = Mathf.Clamp(
            transform.position.z + displacement.z,
            minZ,
            maxZ
        );

        displacement.z = targetZ - transform.position.z;

        if (controller.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2;
            }

            if (jumpPressed)
            {
                velocity.y = jumpForce;
            }
        }

        jumpPressed = false;

        if (velocity.y < 0)
        {
            velocity.y += fallGravity * Time.deltaTime;
        }
        else
        {
            velocity.y += jumpGravity * Time.deltaTime;
        }

        displacement.y = velocity.y * Time.deltaTime;

        controller.Move(displacement);
    }
}
