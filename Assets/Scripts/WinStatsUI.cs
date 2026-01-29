using TMPro;
using UnityEngine;

public class WinStatsUI : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private TMP_Text totalCoinsText;
    [SerializeField] private TMP_Text bestLevel1Text;
    [SerializeField] private TMP_Text bestLevel2Text;
    [SerializeField] private TMP_Text bestLevel3Text;

    private void Start()
    {
        // Total coins this run
        int coins = (CoinManager.Instance != null) ? CoinManager.Instance.TotalCoins : 0;
        if (totalCoinsText != null)
            totalCoinsText.text = $"Total Coins: {coins}";

        // Best times per level (saved across sessions)
        SetBest(bestLevel1Text, "Level1", "Best Level 1");
        SetBest(bestLevel2Text, "Level2", "Best Level 2");
        SetBest(bestLevel3Text, "Level3", "Best Level 3");
    }

    private void SetBest(TMP_Text text, string levelName, string label)
    {
        if (text == null) return;

        float best = PlayerProgressStats.GetBestTime(levelName);
        text.text = $"{label}: {PlayerProgressStats.FormatTime(best)}";
    }
}
