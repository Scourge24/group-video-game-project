using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly Vector2 TargetAspectRatio = new(16, 9);

    private float LastTargetAspectRatio;
    private float LastScreenAspectRatio;

    private void Update()
    {
        float targetAspectRatio = TargetAspectRatio.x / TargetAspectRatio.y;
        float screenAspectRatio = Screen.width / (float)Screen.height;

        if (screenAspectRatio == LastScreenAspectRatio && targetAspectRatio == LastTargetAspectRatio)
        {
            return;
        }

        if (Mathf.Approximately(screenAspectRatio, targetAspectRatio))
        {
            // Current aspect ratio is the same as the target, so render using the entire screen.
            Camera.main.rect = new Rect(0, 0, 1, 1);
        }
        else if (screenAspectRatio > targetAspectRatio)
        {
            // Screen is wider than the target ratio, so render in a pillarbox.
            float normalizedWidth = targetAspectRatio / screenAspectRatio;
            float barThickness = (1f - normalizedWidth) / 2f;
            Camera.main.rect = new Rect(barThickness, 0, normalizedWidth, 1);
        }
        else
        {
            // Screen is narrower than the target ratio, so render in a letterbox.
            float normalizedHeight = screenAspectRatio / targetAspectRatio;
            float barThickness = (1f - normalizedHeight) / 2f;
            Camera.main.rect = new Rect(0, barThickness, 1, normalizedHeight);
        }

        LastTargetAspectRatio = targetAspectRatio;
        LastScreenAspectRatio = screenAspectRatio;
    }
}
