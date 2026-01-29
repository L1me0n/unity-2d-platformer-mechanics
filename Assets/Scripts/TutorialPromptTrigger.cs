using UnityEngine;

public class TutorialPromptTrigger : MonoBehaviour
{
    [TextArea(2, 4)]
    [SerializeField] private string message = "Message here";
    [SerializeField] private float showSeconds = 2f;
    [SerializeField] private bool showOnlyOnce = true;

    private bool hasShown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (showOnlyOnce && hasShown) return;

        hasShown = true;

        if (TutorialPromptUI.Instance != null)
            TutorialPromptUI.Instance.Show(message, showSeconds);
    }
}
