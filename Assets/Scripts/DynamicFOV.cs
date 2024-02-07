using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    public Camera camera; // Assign your main camera here
    public float baseFOV = 90f; // The base FOV at starting speed
    public float maxFOV = 110f; // The maximum FOV at maximum speed
    public float maxSpeed = 20f; // The speed at which FOV reaches its maximum

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main; // Automatically assign the main camera if not set
        }

        // Subscribe to the OnSpeedChanged event
        SpeedController.Instance.OnSpeedChanged += AdjustFOV;
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SpeedController.Instance.OnSpeedChanged -= AdjustFOV;
    }

    void AdjustFOV(float newSpeed)
    {
        // Calculate the new FOV. This maps the range of speeds to the range of FOVs
        float newFOV = Mathf.Lerp(baseFOV, maxFOV, newSpeed / maxSpeed);

        // Clamp the FOV to ensure it doesn't exceed the maximum value
        camera.fieldOfView = Mathf.Clamp(newFOV, baseFOV, maxFOV);
    }
}
