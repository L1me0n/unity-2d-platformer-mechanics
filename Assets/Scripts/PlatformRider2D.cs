using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformRider2D : MonoBehaviour
{
    [SerializeField] private Transform feetPoint;      // put near player's feet
    [SerializeField] private float rayDistance = 0.2f; // small
    [SerializeField] private LayerMask groundMask;     // include platforms/ground

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (feetPoint == null) return;

        // Raycast straight down to detect what player is standing on
        RaycastHit2D hit = Physics2D.Raycast(feetPoint.position, Vector2.down, rayDistance, groundMask);

        if (hit.collider == null) return;

        // If the thing under player is a moving platform, carry by its delta
        MovingPlatform mp = hit.collider.GetComponent<MovingPlatform>();
        if (mp != null)
        {
            rb.position += mp.DeltaThisStep;
        }
    }
}
