using UnityEngine;
using TMPro;

public class PressEPrompt : MonoBehaviour
{
    [Header("Text to show")]
    public TMP_Text pressEText;  // Drag your TMP Text here
    [Header("Player Detection")]
    public string playerTag = "Player"; // Default "Player"

    private void Start()
    {
        if (pressEText != null)
            pressEText.gameObject.SetActive(false); // Hide at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (pressEText != null)
                pressEText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (pressEText != null)
                pressEText.gameObject.SetActive(false);
        }
    }
}
