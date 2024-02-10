using UnityEngine;

public class Car : MonoBehaviour
{
    public float baseMoveSpeed = 5.0f; // Base speed of movement
    private float moveSpeed; // Current speed of movement, including adjustments from SpeedController
    public float despawnDistance = 10.0f; // Distance after which the car will despawn
    private Vector3 startPosition;
    public Vector3 moveDirection = -Vector3.forward; // Default move direction

    void Start()
    {
        startPosition = transform.position; // Remember the start position of the car
        moveSpeed = baseMoveSpeed + SpeedController.Instance.currentSpeed; // Initialize moveSpeed with baseMoveSpeed
        SpeedController.Instance.OnSpeedChanged += HandleSpeedChanged; // Subscribe to speed changes
    }

    void OnDestroy()
    {
        SpeedController.Instance.OnSpeedChanged -= HandleSpeedChanged; // Unsubscribe to prevent memory leaks
    }

    void Update()
    {
        // Move the car forward based on the current move speed
        transform.Translate(-transform.forward * moveSpeed * Time.deltaTime, Space.World);

        // Check if the car has traveled the despawn distance
        if (Vector3.Distance(startPosition, transform.position) >= despawnDistance)
        {
            Destroy(gameObject); // Despawn the car
        }
    }

    void HandleSpeedChanged(float newSpeed)
    {
        // Update the car's speed based on the SpeedController's speed plus the base speed
        // Adjust this formula as needed to fit the desired speed behavior
        moveSpeed = baseMoveSpeed + newSpeed; // Simple linear addition for demonstration
    }
}
