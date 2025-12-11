using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;

    bool isDashing = false;
    float dashTimeRemaining = 0f;
    float dashCooldownTimer = 0f;

    Vector2 lastMoveDirection = Vector2.right;
    Vector2 dashDirection;


    private Rigidbody2D rb;
    private Vector2 moveInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical);

        moveInput = inputVector.normalized;



        if (inputVector.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput;
        }


        HandleDashInput();

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

    private void HandleDashInput()
    {
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
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
}
