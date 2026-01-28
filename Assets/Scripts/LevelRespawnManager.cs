using UnityEngine;

public class LevelRespawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    [Header("Fall Settings")]
    [SerializeField] private float fallY = -10f;

    private Rigidbody2D rb;

    private void Awake()
    {
        if (player != null)
            rb = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player == null || respawnPoint == null) return;

        if (player.position.y < fallY)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        // Move player
        player.position = respawnPoint.position;

        // Reset velocity so player doesn't keep falling forever
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void SetRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
    }
}
