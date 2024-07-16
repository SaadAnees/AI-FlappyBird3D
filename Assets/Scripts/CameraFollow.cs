using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform bird; // Reference to the bird's transform
    public float offsetX = 5f; // Horizontal offset from the bird

    private Vector3 initialPosition;

    void Start()
    {
        // Save the initial position of the camera
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        // Update the camera's x position to follow the bird
        transform.position = new Vector3(bird.position.x + offsetX, initialPosition.y, initialPosition.z);
    }
}
