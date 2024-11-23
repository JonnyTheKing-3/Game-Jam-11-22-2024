using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player
    public Vector3 offset = new Vector3(0, 10, -10); // Adjust this as needed for your isometric view

    void LateUpdate()
    {
        // Update camera position to follow the player's position
        transform.position = player.position + offset;

        // Ensure the camera keeps its rotation
        transform.rotation = Quaternion.Euler(30f, 45f, 0f);
    }
}