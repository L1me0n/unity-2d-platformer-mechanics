using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 70f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float baseGravityScale = 4f;
    [SerializeField] private float fallGravityMultiplier = 1.5f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.18f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ability: Double Jump")]
    [SerializeField] private bool enableDoubleJump = true;
    [SerializeField] private int maxAirJumps = 1; // 1 = double jump

    private Rigidbody2D rb;
    private Animator anim;

    private float moveInput;
    private bool isGrounded;

    private float coyoteTimer;
    private float jumpBufferTimer;

    private int airJumpsRemaining;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 1) Input
        moveInput = Input.GetAxisRaw("Horizontal");

        // 2) Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 3) Reset air jumps when grounded
        if (isGrounded)
        {
            airJumpsRemaining = maxAirJumps;
        }

        // 4) Coyote time timer
        if (isGrounded) coyoteTimer = coyoteTime;
        else coyoteTimer -= Time.deltaTime;

        // 5) Jump buffer timer (Space press)
        if (Input.GetKeyDown(KeyCode.Space)) jumpBufferTimer = jumpBufferTime;
        else jumpBufferTimer -= Time.deltaTime;

        // 6) Jump decision (ONLY if jump was pressed recently)
        if (jumpBufferTimer > 0f)
        {
            // Normal jump (grounded or within coyote)
            if (isGrounded || coyoteTimer > 0f)
            {
                DoJump();
                jumpBufferTimer = 0f;
                coyoteTimer = 0f; // consume coyote so player can't "double-dip"
            }
            // Air jump (double jump)
            else if (enableDoubleJump && airJumpsRemaining > 0)
            {
                airJumpsRemaining--;
                DoJump();
                jumpBufferTimer = 0f;
            }
        }

        // 7) Better fall gravity
        if (rb.linearVelocity.y < 0f)
        {
            rb.gravityScale = baseGravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = baseGravityScale;
        }

        // 8) Animation params (safe)
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
            anim.SetBool("IsGrounded", isGrounded);
            anim.SetFloat("YVelocity", rb.linearVelocity.y);
        }
    }

    private void FixedUpdate()
    {
        float targetSpeed = moveInput * moveSpeed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        float movement = speedDiff * accelRate;
        rb.AddForce(new Vector2(movement, 0f));
    }

    private void DoJump()
    {
        // Reset vertical velocity so jumps feel consistent
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
