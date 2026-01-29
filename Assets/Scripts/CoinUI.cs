using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    private void Awake()
    {
        if (coinsText == null)
            coinsText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (CoinManager.Instance == null) return;
        coinsText.text = $"Coins: {CoinManager.Instance.TotalCoins}";
    }
}
