using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.1f, 0.0f);

    private Material material; // Use Material instead of Renderer

    void Start()
    {
        material = GetComponent<Renderer>().material; // Get the material directly
    }

    void OnEnable()
    {
        PlayerController.OnSpeedChanged += HandleSpeedChanged;
    }

    void OnDisable()
    {
        PlayerController.OnSpeedChanged -= HandleSpeedChanged;
    }

    void HandleSpeedChanged(float newSpeed)
    {
        scrollSpeed = new Vector2(0, -newSpeed); // Update this object's speed based on the player's speed
        // Additional logic to adjust this object's movement based on the new speed
    }

    void Update()
    {
        Vector2 offset = Time.time * scrollSpeed;

        // Ensure you're using the correct property name for the shader you're using
        // "_BaseMap" is commonly used in URP for the main texture, but this might vary based on your shader
        material.SetTextureOffset("_BaseMap", offset);
    }
}
