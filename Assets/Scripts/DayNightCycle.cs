using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    public GameClock clock;         // Drag your GameClock here
    public Light2D globalLight;
    public Camera mainCamera;

    [Header("Colors")]
    public Color dayLightColor = Color.white;
    public Color nightLightColor = new Color(0.1f, 0.1f, 0.35f);
    public Color daySkyColor = new Color(0.5f, 0.8f, 1f);
    public Color nightSkyColor = new Color(0.05f, 0.05f, 0.1f);

    [Header("Brightness Curve")]
    public AnimationCurve brightnessOverDay = new AnimationCurve(
        new Keyframe(7f, 0.7f),   // 7 AM → 70%
        new Keyframe(12f, 1f),    // 12 PM → 100%
        new Keyframe(18.5f, 0.5f),// 6:30 PM → 50%
        new Keyframe(20f, 0.2f)   // 8 PM → 20%
    );

    void Update()
    {
        if (clock == null || globalLight == null) return;

        float hour = clock.GetCurrentHour();

        // Sample brightness from the curve
        float brightness = brightnessOverDay.Evaluate(hour);

        // Apply to light
        globalLight.intensity = brightness;
        globalLight.color = Color.Lerp(nightLightColor, dayLightColor, brightness);

        // Apply to camera background
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = Color.Lerp(nightSkyColor, daySkyColor, brightness);
        }
    }
}
