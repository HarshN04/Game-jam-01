using UnityEngine;
using TMPro;

public class GameClock : MonoBehaviour
{
    [Header("Clock Settings")]
    public int startHour = 7;              
    public int endHour = 20;               
    public float realSecondsPerHour = 60f; 

    [Header("UI Reference")]
    public TMP_Text clockText; // TMP text for clock + day

    private int currentHour;
    private int currentMinute;
    private int currentDay = 1;   // day counter
    private float timeCounter;

    private void Start()
    {
        currentHour = startHour;
        currentMinute = 0;
        timeCounter = 0f;
        UpdateClockUI();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= realSecondsPerHour / 60f)
        {
            timeCounter = 0f;
            AddMinute();
        }
    }

    private void AddMinute()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
        }

        if (currentHour > endHour)
        {
            currentHour = startHour;
            currentMinute = 0;
            AdvanceToNextDay();  // increment day automatically if needed
        }

        UpdateClockUI();
    }

    private void UpdateClockUI()
    {
        if (clockText != null)
        {
            string ampm = currentHour >= 12 ? "PM" : "AM";
            int displayHour = currentHour > 12 ? currentHour - 12 : currentHour;
            if (displayHour == 0) displayHour = 12;

            clockText.text = $"Day {currentDay}/7  {displayHour:00}:{currentMinute:00} {ampm}";
        }
    }

    public float GetCurrentHour()
    {
        return currentHour + (currentMinute / 60f);
    }

    // Call this when the player sleeps
    public void AdvanceToNextDay()
    {
        currentDay++;
        if (currentDay > 7) currentDay = 7; // cap at 7
        currentHour = startHour;
        currentMinute = 0;
        UpdateClockUI();
    }
}
