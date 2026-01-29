using UnityEngine;

public class LevelStartResetCoins : MonoBehaviour
{
    private void Start()
    {
        if (CoinManager.Instance == null) return;

        CoinManager.Instance.ResetRun();
        CoinManager.Instance.SaveSnapshot(); // now default spawn snapshot = clean run start
    }
}
