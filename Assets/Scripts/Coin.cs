using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;

    // Runtime-unique key per coin instance
    private string key;

    public string Key => key;
    public int Value => value;

    private void Awake()
    {
        // Scene buildIndex makes it safer across scene changes,
        // GetInstanceID makes it unique within the scene.
        int sceneIndex = gameObject.scene.buildIndex;
        key = $"{sceneIndex}_{GetInstanceID()}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (CoinManager.Instance == null) return;

        CoinManager.Instance.CollectCoin(this);
    }
}
