using UnityEngine;

public class PlayerSpriteRotation : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Reference to the camera
    private Vector3 lastCameraPosition; // Store the camera position from the last frame

    void Start()
    {
        // Initialize the last camera position at the start
        lastCameraPosition = Vector3.zero;
    }

    void LateUpdate()
    {
        // Check if the camera has moved
        if (cameraTransform.position != lastCameraPosition)
        {
            // Get the vector from the sprite to the camera, ignoring the y-axis (height)
            Vector3 directionToCamera = cameraTransform.position - transform.position;
            directionToCamera.y = 0f; // Keep the rotation on the XZ plane (ignore height)

            // Calculate the desired rotation to face the camera
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Apply the rotation to face the camera
            transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

            // Update the last camera position
            lastCameraPosition = cameraTransform.position;
        }
    }
}