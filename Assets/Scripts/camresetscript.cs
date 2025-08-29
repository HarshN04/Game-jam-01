using UnityEngine;

public class CameraSafetyReset : MonoBehaviour
{
    void LateUpdate()
    {
        // Force the Camera's scale to stay normal
        transform.localScale = Vector3.one;
    }
}

