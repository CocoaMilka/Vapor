using System;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController Instance { get; private set; }
    public float currentSpeed = 1.0f; // Default speed
    public float maxSpeed = 10.0f; // Maximum speed limit
    public float minSpeed = 0.5f; // Minimum speed limit
    public event Action<float> OnSpeedChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep instance alive across scenes
        }
    }

    private void Update()
    {
        Debug.Log(currentSpeed);
    }

    // Method to call when increasing speed
    public void IncreaseSpeed(float amount)
    {
        currentSpeed += amount;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed); // Clamp speed to ensure it's within min and max bounds
        OnSpeedChanged?.Invoke(currentSpeed);
    }

    // Method to call when decreasing speed
    public void DecreaseSpeed(float amount)
    {
        currentSpeed -= amount;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed); // Clamp speed to ensure it's within min and max bounds
        OnSpeedChanged?.Invoke(currentSpeed);
    }
}
