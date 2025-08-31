using UnityEngine;
using TMPro;

public class GameClock : MonoBehaviour
{
    [Header("Clock Settings")]
    public int startHour = 7;               // 7 AM
    public int endHour = 20;                // 8 PM
    public float realSecondsPerHour = 60f;  // 1 in-game hour = 60 real seconds

    [Header("UI Reference")]
    public TMP_Text clockText;  // TMP text for clock and day
    public int totalDays = 7;

    [HideInInspector] public int currentDay = 1;

    private int currentHour;
    private int currentMinute;
    private float timeCounter;

    void Start()
    {
        currentHour = startHour;
        currentMinute = 0;
        timeCounter = 0f;
        UpdateClockUI();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= realSecondsPerHour / 60f)
        {
            timeCounter = 0f;
            AddMinute();
        }
    }

    void AddMinute()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
        }

        // Auto reset at end hour
        if (currentHour > endHour)
        {
            AdvanceDay();
        }

        UpdateClockUI();
    }

    public void AdvanceDay()
    {
        currentDay++;
        if (currentDay > totalDays)
            currentDay = totalDays; // Or handle game end here

        currentHour = startHour;
        currentMinute = 0;

        UpdateClockUI();
    }

    public float GetCurrentHour()
    {
        return currentHour + (currentMinute / 60f);
    }

    void UpdateClockUI()
    {
        if (clockText != null)
        {
            string ampm = currentHour >= 12 ? "PM" : "AM";
            int displayHour = currentHour > 12 ? currentHour - 12 : currentHour;
            if (displayHour == 0) displayHour = 12;

            clockText.text = $"Day {currentDay}/{totalDays} - {displayHour:00}:{currentMinute:00} {ampm}";
        }
    }
}
