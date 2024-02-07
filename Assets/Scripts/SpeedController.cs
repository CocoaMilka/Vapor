using System;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController Instance { get; private set; }
    public float currentSpeed = 1.0f; // Default speed
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
        OnSpeedChanged?.Invoke(currentSpeed);
    }

    // Method to call when decreasing speed
    public void DecreaseSpeed(float amount)
    {
        currentSpeed = Mathf.Max(0, currentSpeed - amount); // Prevent negative speed
        OnSpeedChanged?.Invoke(currentSpeed);
    }
}
