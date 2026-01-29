using UnityEngine;

public static class PlayerProgressStats
{
    private static string KeyBestTime(string levelName) => $"bestTime_{levelName}";

    // Save best time (lower is better)
    public static void RecordLevelTime(string levelName, float timeSeconds)
    {
        if (timeSeconds <= 0f) return;

        string key = KeyBestTime(levelName);

        // If no record yet, default to -1
        float currentBest = PlayerPrefs.GetFloat(key, -1f);

        if (currentBest < 0f || timeSeconds < currentBest)
        {
            PlayerPrefs.SetFloat(key, timeSeconds);
            PlayerPrefs.Save();
        }
    }

    public static float GetBestTime(string levelName)
    {
        return PlayerPrefs.GetFloat(KeyBestTime(levelName), -1f);
    }

    public static string FormatTime(float seconds)
    {
        if (seconds < 0f) return "--:--.--";

        int minutes = Mathf.FloorToInt(seconds / 60f);
        float sec = seconds - minutes * 60f;

        // mm:ss.ff
        return $"{minutes:00}:{sec:00.00}";
    }
}
