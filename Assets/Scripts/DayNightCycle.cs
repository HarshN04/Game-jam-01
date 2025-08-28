using UnityEngine;
using UnityEngine.Rendering.Universal; // Needed for Light2D

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    public float dayLengthInSeconds = 60f;  // full day-night duration
    [Range(0f, 1f)]
    public float timeOfDay = 0f;  // 0 = sunrise, 0.5 = sunset, 1 = back to sunrise

    [Header("Lighting")]
    public Light2D globalLight;
    public Color dayLightColor = Color.white;
    public Color nightLightColor = new Color(0.1f, 0.1f, 0.35f);

    [Header("Sky Colors")]
    public Camera mainCamera;
    public Color daySkyColor = new Color(0.5f, 0.8f, 1f);  // blue sky
    public Color nightSkyColor = new Color(0.05f, 0.05f, 0.1f); // dark blue/black

    void Update()
    {
        // Advance time
        timeOfDay += Time.deltaTime / dayLengthInSeconds;
        if (timeOfDay > 1f) timeOfDay = 0f;

        // Adjust light intensity and color
        float intensity = Mathf.Cos(timeOfDay * Mathf.PI * 2f) * 0.5f + 0.5f;
        globalLight.intensity = Mathf.Lerp(0.2f, 1f, intensity);
        globalLight.color = Color.Lerp(nightLightColor, dayLightColor, intensity);

        // Adjust sky background
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = Color.Lerp(nightSkyColor, daySkyColor, intensity);
        }
    }
}
