using UnityEngine;
using TMPro; // Add this

public class GameClock : MonoBehaviour
{
    [Header("Clock Settings")]
    public int startHour = 7;              
    public int endHour = 20;               
    public float realSecondsPerHour = 60f; 

    [Header("UI Reference")]
    public TMP_Text clockText; // <-- Use TMP_Text instead of Text

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
        // Advance time
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

        if (currentHour > endHour)
        {
            currentHour = startHour;
            currentMinute = 0;
        }

        UpdateClockUI();
    }

    void UpdateClockUI()
    {
        if (clockText != null)
        {
            string ampm = currentHour >= 12 ? "PM" : "AM";
            int displayHour = currentHour > 12 ? currentHour - 12 : currentHour;
            if (displayHour == 0) displayHour = 12;

            clockText.text = string.Format("{0:00}:{1:00} {2}", displayHour, currentMinute, ampm);
        }
    }

    public float GetCurrentHour()
    {
        return currentHour + (currentMinute / 60f);
    }
}
