using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerDeath death = other.GetComponent<PlayerDeath>();
        if (death != null)
        {
            death.Kill();
        }
        else
        {
            // fallback if you forgot to add PlayerDeath
            RespawnManager.Instance.Respawn();
        }
    }
}
