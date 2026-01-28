using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead = false;

    public void Kill()
    {
        if (isDead) return;
        isDead = true;

        // You can add screen flash / sound later (Phase 9)
        RespawnManager.Instance.Respawn();

        isDead = false;
    }
}
