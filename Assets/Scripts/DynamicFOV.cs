using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    public Camera camera; // Assign your main camera here
    public float baseFOV = 90f; // The base FOV at starting speed
    public float maxFOV = 110f; // The maximum FOV at maximum speed
    public float maxSpeed = 20f; // The speed at which FOV reaches its maximum
    public float shakeMagnitude = 0.1f; // Maximum shake intensity

    private Vector3 originalCamPos; // To store the camera's original position
    private float currentShakeIntensity = 0f; // Current shake intensity based on speed

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main; // Automatically assign the main camera if not set
        }
        originalCamPos = camera.transform.localPosition;

        // Subscribe to the OnSpeedChanged event
        SpeedController.Instance.OnSpeedChanged += AdjustFOVAndShake;
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SpeedController.Instance.OnSpeedChanged -= AdjustFOVAndShake;
    }

    void AdjustFOVAndShake(float newSpeed)
    {
        // Calculate the new FOV. This maps the range of speeds to the range of FOVs
        float newFOV = Mathf.Lerp(baseFOV, maxFOV, newSpeed / maxSpeed);
        camera.fieldOfView = Mathf.Clamp(newFOV, baseFOV, maxFOV); // Clamp the FOV

        // Update shake intensity based on how close the speed is to maxSpeed
        currentShakeIntensity = Mathf.Lerp(0f, shakeMagnitude, newSpeed / maxSpeed);
    }

    void Update()
    {
        // Apply continuous camera shake based on the current shake intensity
        if (currentShakeIntensity > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * currentShakeIntensity;
            camera.transform.localPosition = originalCamPos + shakeOffset;
        }
        else
        {
            // Reset to original position if there's no shake
            camera.transform.localPosition = originalCamPos;
        }
    }
}
