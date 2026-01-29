using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int TotalCoins { get; private set; }

    // Which coins are currently collected in THIS run
    private HashSet<string> collectedKeys = new HashSet<string>();

    // Coins in the currently loaded scene
    private Dictionary<string, Coin> sceneCoins = new Dictionary<string, Coin>();

    // Snapshot for respawn (default spawn or checkpoint)
    private int snapshotTotalCoins = 0;
    private HashSet<string> snapshotCollectedKeys = new HashSet<string>();

    // Snapshot of coin state when the current level was first entered
    private int levelEntryTotalCoins = 0;
    private HashSet<string> levelEntryCollectedKeys = new HashSet<string>();

    private int lastSceneIndex = -1;

    private bool isRestoring = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If we entered a NEW level (not reloading the same one), capture "level entry" state
        if (scene.buildIndex != lastSceneIndex)
        {
            lastSceneIndex = scene.buildIndex;
            levelEntryTotalCoins = TotalCoins;
            levelEntryCollectedKeys = new HashSet<string>(collectedKeys);
        }

        // New scene = new coins list
        sceneCoins.Clear();

        // Register every coin in the scene (including inactive ones)
        Coin[] coins = Object.FindObjectsByType<Coin>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var coin in coins)
        {
            RegisterCoin(coin);
        }
    }

    private void RegisterCoin(Coin coin)
    {
        if (coin == null) return;

        sceneCoins[coin.Key] = coin;

        // If it was collected, hide it. Otherwise show it.
        bool collected = collectedKeys.Contains(coin.Key);

        // Avoid coin scripts firing back into manager mid-restore
        if (isRestoring) return;

        coin.gameObject.SetActive(!collected);
    }

    public void CollectCoin(Coin coin)
    {
        if (coin == null) return;

        // Already collected? do nothing
        if (collectedKeys.Contains(coin.Key)) return;

        collectedKeys.Add(coin.Key);
        TotalCoins += coin.Value;

        // Disable so it can come back on restore
        coin.gameObject.SetActive(false);
    }

    public void SaveSnapshot()
    {
        snapshotTotalCoins = TotalCoins;
        snapshotCollectedKeys = new HashSet<string>(collectedKeys);
    }

    public void RestoreSnapshot()
    {
        isRestoring = true;

        TotalCoins = snapshotTotalCoins;
        collectedKeys = new HashSet<string>(snapshotCollectedKeys);

        // Safe enumeration
        var coinsList = new List<Coin>(sceneCoins.Values);
        foreach (var coin in coinsList)
        {
            if (coin == null) continue;

            bool collected = collectedKeys.Contains(coin.Key);
            coin.gameObject.SetActive(!collected);
        }

        isRestoring = false;
    }

    public void ResetRun()
    {
        TotalCoins = 0;
        collectedKeys.Clear();

        snapshotTotalCoins = 0;
        snapshotCollectedKeys.Clear();

        // Re-enable coins in current scene
        foreach (var coin in sceneCoins.Values)
        {
            if (coin != null) coin.gameObject.SetActive(true);
        }
    }
    public void RestoreLevelEntryState()
    {
        TotalCoins = levelEntryTotalCoins;
        collectedKeys = new HashSet<string>(levelEntryCollectedKeys);
    }

}
