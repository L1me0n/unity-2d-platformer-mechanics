using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeElapsed;
    private bool isRunning = true;

    private void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime;

        // One decimal looks clean
        timerText.text = $"Time: {timeElapsed:0.0}s";
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetTime()
    {
        return timeElapsed;
    }
}
