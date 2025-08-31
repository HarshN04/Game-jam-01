using UnityEngine;
using TMPro;
using System.Collections;

public class HouseSleepTrigger : MonoBehaviour
{
    [Header("References")]
    public GameClock gameClock;       // Your GameClock script
    public SceneFader sceneFader;     // Reference to your SceneFader
    public Transform playerSpawnPoint; // Where player spawns after sleep
    public TMP_Text sleepingText;     // Text for "Sleeping through the night"
    public TMP_Text pressEText;       // Text prompt "Press E to Sleep"

    [Header("Settings")]
    public int sleepStartHour = 19;   // 7 PM
    public int sleepEndHour = 21;     // 9 PM

    private bool playerInRange = false;
    private bool isSleeping = false;

    private Transform player;

    void Start()
    {
        if (pressEText != null)
            pressEText.gameObject.SetActive(false);

        if (sleepingText != null)
            sleepingText.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (playerInRange && !isSleeping)
        {
            if (pressEText != null)
                pressEText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                float hour = gameClock.GetCurrentHour();
                if (hour >= sleepStartHour && hour <= sleepEndHour)
                {
                    StartCoroutine(SleepRoutine());
                }
            }
        }

        // Auto-sleep at 9 PM
        if (!playerInRange && !isSleeping)
        {
            float hour = gameClock.GetCurrentHour();
            if (hour >= sleepEndHour)
            {
                StartCoroutine(SleepRoutine());
            }
        }
    }

    private IEnumerator SleepRoutine()
    {
        isSleeping = true;

        if (pressEText != null)
            pressEText.gameObject.SetActive(false);

        if (sceneFader != null)
            yield return StartCoroutine(sceneFader.FadeOutCoroutine());

        if (sleepingText != null)
            sleepingText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f); // display "Sleeping" text briefly

        // Increment day and reset clock
        gameClock.AdvanceDay();

        // Move player to spawn point
        if (player != null && playerSpawnPoint != null)
        {
            player.position = playerSpawnPoint.position;
        }

        yield return new WaitForSeconds(1f);

        if (sleepingText != null)
            sleepingText.gameObject.SetActive(false);

        if (sceneFader != null)
            yield return StartCoroutine(sceneFader.FadeInCoroutine());

        isSleeping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEText != null)
                pressEText.gameObject.SetActive(false);
        }
    }
}
