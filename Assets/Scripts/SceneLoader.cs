using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Loads a scene by its exact name
    public void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    // Reloads whatever scene is currently active
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;

        // Rewind coins to level-entry state BEFORE reloading
        if (CoinManager.Instance != null)
            CoinManager.Instance.RestoreLevelEntryState();

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    // Quits the game (works in builds, not in editor)
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("QuitGame() called. This only actually quits in a built game.");
    }
}
