using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 70f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float coyoteTime = 0.1f; //jump slightly after leaving ground
    [SerializeField] private float jumpBufferTime = 0.1f; //press jump slightly before landing
    [SerializeField] private float fallGravityMultiplier = 1.5f; //snappier falling

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.18f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    private Animator anim;

    private float moveInput;
    private bool isGrounded;

    private float coyoteTimer;
    private float jumpBufferTimer;

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

        // 3) Coyote time timer
        if (isGrounded) coyoteTimer = coyoteTime;
        else coyoteTimer -= Time.deltaTime;

        // 4) Jump buffer timer
        if (Input.GetKeyDown(KeyCode.Space)) jumpBufferTimer = jumpBufferTime;
        else jumpBufferTimer -= Time.deltaTime;

        // 5) Jump if both timers allow it
        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            Jump();
            jumpBufferTimer = 0f; // consume
        }

        // 6) Better fall
        if (rb.linearVelocity.y < 0f)
        {
            rb.gravityScale = 4f * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = 4f;
        }

        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
            anim.SetBool("IsGrounded", isGrounded);
            anim.SetFloat("YVelocity", rb.linearVelocity.y);
        }


    }

    private void FixedUpdate()
    {
        // Target speed
        float targetSpeed = moveInput * moveSpeed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;

        // Accelerate vs decelerate
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        // Apply force to reach target speed smoothly
        float movement = speedDiff * accelRate;

        rb.AddForce(new Vector2(movement, 0f));
    }

    private void Jump()
    {
        // Reset vertical velocity so jumps feel consistent
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // After jump, no more coyote
        coyoteTimer = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
