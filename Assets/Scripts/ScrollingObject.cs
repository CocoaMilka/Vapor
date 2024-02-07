using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    //public Vector3 moveDirection = new Vector3(0, 0, 1); // Default movement along the Z-axis
    public Vector3 moveDirection;
    public float moveSpeed = 5.0f; // Speed of movement
    public float despawnZDistance = 10.0f; // Distance after which the object will despawn
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Remember the start position of the object
        moveDirection = -transform.forward;
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
        moveSpeed = newSpeed; // Update this object's speed based on the player's speed
        // Additional logic to adjust this object's movement based on the new speed
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object in the specified direction at the specified speed
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        // Check the distance moved along the Z-axis
        if (Mathf.Abs(transform.position.z - startPosition.z) >= despawnZDistance)
        {
            //Destroy(gameObject); // Despawn the object
            transform.position = startPosition;
        }
    }
}
