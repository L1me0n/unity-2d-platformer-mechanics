using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool singleUse = true;

    private bool activated = false;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated && singleUse) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            RespawnManager.Instance.SetCheckpoint(transform.position);

            // Simple feedback so you KNOW it worked
            if (spriteRenderer != null)
                spriteRenderer.color = Color.green;
                transform.localScale = Vector3.one * 1.2f;
        }
    }
}
