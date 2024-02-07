using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    private Material material; // Material whose texture will be scrolled
    private Vector2 offset = Vector2.zero; // Current accumulated offset
    private float speed; // Current speed for scrolling

    void Start()
    {
        material = GetComponent<Renderer>().material; // Get the material directly
        speed = -SpeedController.Instance.currentSpeed; // Initialize speed from the SpeedController
        SpeedController.Instance.OnSpeedChanged += HandleSpeedChanged; // Subscribe to speed changes
    }

    void OnDestroy()
    {
        if (SpeedController.Instance != null)
        {
            SpeedController.Instance.OnSpeedChanged -= HandleSpeedChanged; // Unsubscribe to prevent memory leaks
        }
    }

    void HandleSpeedChanged(float newSpeed)
    {
        speed = -newSpeed; // Update speed based on the player's speed, negative for scrolling down
    }

    void Update()
    {
        // Accumulate the offset based on the current speed and time elapsed since last frame
        offset.y += speed * Time.deltaTime;

        // Apply the accumulated offset to the material's texture
        material.mainTextureOffset = offset;
    }
}
