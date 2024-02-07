using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public Vector3 moveDirection;
    public float baseMoveSpeed = 5.0f; // Base speed of movement
    private float moveSpeed; // Current speed of movement, including adjustments from SpeedController
    public float despawnZDistance = 10.0f; // Distance after which the object will despawn
    public float speedMultiplier = 10.0f; // Speed of scrolling textures are different from physically moving objects
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Remember the start position of the object
        moveDirection = -transform.forward;
        moveSpeed = baseMoveSpeed; // Initialize moveSpeed with baseMoveSpeed
    }

    void OnEnable()
    {
        SpeedController.Instance.OnSpeedChanged += HandleSpeedChanged;
    }

    void OnDisable()
    {
        SpeedController.Instance.OnSpeedChanged -= HandleSpeedChanged;
    }

    void HandleSpeedChanged(float newSpeed)
    {
        // Update this object's speed based on the player's speed plus the base speed
        moveSpeed = baseMoveSpeed + Mathf.Pow(newSpeed, 2) * speedMultiplier; // Example of a non-linear scaling

    }

    void Update()
    {
        // Move the object in the specified direction at the current move speed
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        // Check the distance moved along the Z-axis
        if (Mathf.Abs(transform.position.z - startPosition.z) >= despawnZDistance)
        {
            // Instead of destroying, reset position to create a looping effect
            transform.position = startPosition;
        }
    }
}
