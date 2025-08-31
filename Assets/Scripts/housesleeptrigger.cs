using UnityEngine;
using TMPro;
using System.Collections;

public class HouseSleepTrigger : MonoBehaviour
{
    public GameClock clock;
    public Transform spawnPoint;
    public TMP_Text sleepingText;  // assign your "Sleeping through the night" text
    public float fadeDuration = 1f;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            float hour = clock.GetCurrentHour();
            if (hour >= 19f && hour <= 21f) // only sleep 7PM to 9PM
                StartCoroutine(SleepRoutine());
        }
    }

    private IEnumerator SleepRoutine()
    {
        if (sleepingText != null)
            sleepingText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f); // show text briefly

        clock.AdvanceToNextDay(); // increment day
        yield return new WaitForSeconds(0.5f);

        if (sleepingText != null)
            sleepingText.gameObject.SetActive(false);

        // move player to spawn
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && spawnPoint != null)
            player.transform.position = spawnPoint.position;
    }
}
