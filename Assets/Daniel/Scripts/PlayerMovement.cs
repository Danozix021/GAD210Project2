using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;

    [Header("Input Settings")]
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private string verticalAxis = "Vertical";
    [SerializeField] private string dashButton = "Jump"; 
    [SerializeField] private float inputDeadZone = 0.2f; 

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool isDashing = false;
    private float dashTimeRemaining = 0f;
    private float dashCooldownTimer = 0f;
    private Vector2 lastMoveDirection = Vector2.right;
    private Vector2 dashDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReadMovementInput();
        HandleDashInputAndTimers();
    }

    private void ReadMovementInput()
    {
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        Vector2 inputVector = new Vector2(horizontal, vertical);

      
        if (inputVector.magnitude < inputDeadZone)
        {
            inputVector = Vector2.zero;
        }

        moveInput = inputVector.normalized;

        
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput;
        }
    }

    private void HandleDashInputAndTimers()
    {
      
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

       
        if (Input.GetButtonDown(dashButton))
        {
            TryStartDash();
        }

        if (isDashing)
        {
            dashTimeRemaining -= Time.deltaTime;
            if (dashTimeRemaining <= 0f)
            {
                isDashing = false;
            }
        }
    }

    private void TryStartDash()
    {
        if (isDashing || dashCooldownTimer > 0f)
            return;

        if (lastMoveDirection.sqrMagnitude < 0.01f)
            return;

        isDashing = true;
        dashTimeRemaining = dashDuration;
        dashCooldownTimer = dashCooldown;
        dashDirection = lastMoveDirection;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    public Vector2 GetLastMoveDirection()
    {
        return lastMoveDirection;
    }

}
