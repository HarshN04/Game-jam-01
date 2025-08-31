using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialoguePanel;    // assign Panel
    public TMP_Text dialogueText;       // assign TextMeshPro
    public GameObject interactPrompt;   // "Press E" text above player

    [Header("Dialogue")]
    [TextArea(3, 5)]
    public string[] dialogueLines; // multiple lines of dialogue
    private int currentLine = 0;

    private bool playerInRange = false;
    private bool dialogueActive = false;

    void Update()
    {
        if (playerInRange && !dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
        else if (dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        dialoguePanel.SetActive(true);
        interactPrompt.SetActive(false);

        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            interactPrompt.SetActive(false);
        }
    }
}
