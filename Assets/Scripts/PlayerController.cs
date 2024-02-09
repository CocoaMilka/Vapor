using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 1.4f; // Rate at which speed increases
    public float deceleration = 1.5f; // Rate at which speed decreases
    public float tiltAmount = 15.0f; // Maximum tilt angle
    public float tiltSpeed = 5.0f; // Speed of tilting

    public GameObject carBody; // Car body to tilt

    private float currentSpeed; // To keep track of the current speed based on the SpeedController

    void Awake()
    {
        // Initialize current speed from the SpeedController at start
        currentSpeed = SpeedController.Instance.currentSpeed;
    }

    void Update()
    {
        float tilt = 0; // Reset tilt to 0 each frame

        // Move player left with A key
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -2) // Check for left boundary
            {
                transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
                tilt = tiltAmount; // Tilt left when moving left
            }
        }

        // Move player right with D key
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 2) // Check for right boundary
            {
                transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
                tilt = -tiltAmount; // Tilt right when moving right
            }
        }

        // Increase speed when W key is held
        if (Input.GetKey(KeyCode.W))
        {
            SpeedController.Instance.IncreaseSpeed(acceleration * Time.deltaTime);
        }

        // Decrease speed when S key is held
        if (Input.GetKey(KeyCode.S))
        {
            SpeedController.Instance.DecreaseSpeed(deceleration * Time.deltaTime);
        }

        // Update the current speed from the SpeedController to ensure it's in sync
        currentSpeed = SpeedController.Instance.currentSpeed;

        // Interpolate carBody rotation towards target rotation based on tilt
        if (carBody != null)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, tilt);
            carBody.transform.localRotation = Quaternion.Lerp(carBody.transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);
        }
    }
}
