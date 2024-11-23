using UnityEngine;

public class FixDirectionOrientation : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraTransform;

    void LateUpdate()
    {
        // Get camera's forward vector flattened onto the XZ plane
        Vector3 flatForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;

        // Align playerOrientation with the flattened camera forward
        player.forward = flatForward;
    }
}