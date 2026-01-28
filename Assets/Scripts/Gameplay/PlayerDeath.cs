using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead = false;

    public void Kill()
    {
        if (isDead) return;
        isDead = true;

        RespawnManager.Instance.Respawn();

        isDead = false;
    }
}
