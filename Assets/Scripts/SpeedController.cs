using System;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static float currentSpeed = 5.0f; // Default speed
    public static event Action<float> OnSpeedChanged;

    // Method to call when increasing speed
    public void IncreaseSpeed(float amount)
    {
        currentSpeed += amount;
        OnSpeedChanged?.Invoke(currentSpeed);
    }

    // Method to call when decreasing speed
    public void DecreaseSpeed(float amount)
    {
        currentSpeed = Mathf.Max(0, currentSpeed - amount); // Prevent negative speed
        OnSpeedChanged?.Invoke(currentSpeed);
    }
}
