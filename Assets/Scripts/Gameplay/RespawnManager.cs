using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Respawn")]
    [SerializeField] private Transform defaultSpawnPoint; // level start spawn
    private Vector2 currentSpawnPosition;

    [Header("Fall Death")]
    [SerializeField] private bool useFallDeath = true;
    [SerializeField] private float fallY = -15f;

    private void Awake()
    {
        // Singleton pattern: one instance per scene
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (playerRb == null && player != null)
            playerRb = player.GetComponent<Rigidbody2D>();

        // Set initial spawn
        if (defaultSpawnPoint != null)
            currentSpawnPosition = defaultSpawnPoint.position;
        else if (player != null)
            currentSpawnPosition = player.position;
        
        if (CoinManager.Instance != null)
            CoinManager.Instance.SaveSnapshot(); // default spawn coin state

    }

    private void Update()
    {
        if (!useFallDeath || player == null) return;

        if (player.position.y < fallY)
        {
            Respawn();
        }
    }

    public void SetCheckpoint(Vector2 newSpawnPos)
    {
        currentSpawnPosition = newSpawnPos;
        if (CoinManager.Instance != null)
            CoinManager.Instance.SaveSnapshot(); // checkpoint coin state

    }

    public void Respawn()
    {
        if (CoinManager.Instance != null)
            CoinManager.Instance.RestoreSnapshot();

        if (player == null) return;

        // Teleport player
        player.position = currentSpawnPosition;

        // Reset velocity so player doesn't keep falling/sliding
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;
        }
    }
}
