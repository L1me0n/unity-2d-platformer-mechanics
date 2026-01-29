using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialPromptUI : MonoBehaviour
{
    public static TutorialPromptUI Instance { get; private set; }

    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text promptText;

    [Header("Timing")]
    [SerializeField] private float fadeDuration = 0.2f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);


        if (canvasGroup == null) canvasGroup = GetComponentInChildren<CanvasGroup>(true);
        if (promptText == null) promptText = GetComponentInChildren<TMP_Text>(true);

        HideInstant();
    }

    public void Show(string message, float visibleSeconds = 2f)
    {
        if (canvasGroup == null || promptText == null) return;

        promptText.text = message;

        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ShowRoutine(visibleSeconds));
    }

    public void HideInstant()
    {
        if (canvasGroup == null) return;
        canvasGroup.alpha = 0f;
    }

    private IEnumerator ShowRoutine(float visibleSeconds)
    {
        // Fade in (Realtime so pause doesn't freeze it)
        yield return FadeTo(1f);

        // Stay visible
        if (visibleSeconds > 0f)
            yield return new WaitForSecondsRealtime(visibleSeconds);

        // Fade out
        yield return FadeTo(0f);
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime; // works during pause
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
