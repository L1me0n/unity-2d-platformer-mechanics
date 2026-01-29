using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Level2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Grab timer from the level, stop it, record best time
        LevelTimer timer = FindFirstObjectByType<LevelTimer>();
        if (timer != null)
        {
            timer.StopTimer();
            string currentLevelName = SceneManager.GetActiveScene().name;
            PlayerProgressStats.RecordLevelTime(currentLevelName, timer.GetTime());
        }

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
