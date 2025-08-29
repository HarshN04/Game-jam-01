using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // The player
    public float pixelsPerUnit = 16f; // Match your sprite import setting (PPU)
    public Vector3 offset = new Vector3(0, 0, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        // Follow target
        Vector3 newPos = target.position + offset;

        // Snap camera position to pixel grid
        float unitsPerPixel = 1f / pixelsPerUnit;
        newPos.x = Mathf.Round(newPos.x / unitsPerPixel) * unitsPerPixel;
        newPos.y = Mathf.Round(newPos.y / unitsPerPixel) * unitsPerPixel;

        transform.position = newPos;
    }
}
