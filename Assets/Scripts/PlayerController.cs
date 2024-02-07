using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SpeedChanged(float newSpeed);

public class PlayerController : MonoBehaviour
{
    public static event SpeedChanged OnSpeedChanged; // Static event invoked when speed changes

    public float speed = 5.0f; // Speed at which the player moves
    public float speedChangeAmount = 0.01f; // Amount by which speed increases or decreases
    public float tiltAmount = 15.0f; // Maximum tilt angle
    public float tiltSpeed = 5.0f; // Speed of tilting

    public GameObject carBody; // Car body to tilt

    void Update()
    {
        float tilt = 0; // Reset tilt to 0 each frame

        // Move player left with A key
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x <= -2)
            {
                // Do nothing, wall collision
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                tilt = tiltAmount; // Set tilt to maximum tilt angle when moving left
            }
        }

        // Move player right with D key
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x >= 2)
            {
                // Do nothing, wall collision
            }
            else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                tilt = -tiltAmount; // Set tilt to negative maximum tilt angle when moving right
            }
        }

        // Increase speed when W key is pressed
        if (Input.GetKey(KeyCode.W))
        {
            speed += speedChangeAmount; // Increase the speed
            if (speed > 10) speed = 10;
            Debug.Log("Current Speed: " + speed);
            OnSpeedChanged?.Invoke(speed); // Invoke the event, passing the new speed as an argument
        }

        // Decrease speed when S key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            speed -= speedChangeAmount; // Decrease the speed
            if (speed < 3) speed = 3; // Prevent speed from going negative
            OnSpeedChanged?.Invoke(speed); // Invoke the event, passing the new speed as an argument
        }

        // Interpolate carBody rotation towards target rotation based on tilt
        if (carBody != null)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, tilt); // Assuming Z-axis is the correct tilt axis
            carBody.transform.localRotation = Quaternion.Lerp(carBody.transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);
        }
    }
}
