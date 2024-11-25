using UnityEngine;

public class TireSwinging : MonoBehaviour
{
    public Transform pivotPoint; // The center point of the swinging motion
    public float swingRadius = 2f; // Radius of the swing arc
    public float speedOfSwing = 1f; // Speed of the swinging motion
    public float angleRange = 45f; // Maximum angle of the swing in degrees

    private float currentAngle = 0f; // Current angle of the swing
    private bool swingingToPositive = true; // Direction of the swing

    void Update()
    {
        // Adjust the current angle based on the swinging direction
        float angleDelta = speedOfSwing * Time.deltaTime;
        currentAngle += swingingToPositive ? angleDelta : -angleDelta;

        // Clamp the angle within the range
        if (currentAngle >= angleRange)
        {
            currentAngle = angleRange;
            swingingToPositive = false;
        }
        else if (currentAngle <= -angleRange)
        {
            currentAngle = -angleRange;
            swingingToPositive = true;
        }

        // Calculate the new position
        Vector3 offset = new Vector3(
            0, // No movement on the X-axis
            -Mathf.Cos(currentAngle * Mathf.Deg2Rad) * swingRadius, // Y offset (downward motion)
            swingRadius * Mathf.Sin(currentAngle * Mathf.Deg2Rad) // Z offset (swinging motion)
        );

        // Set the position of the swinging object
        transform.position = pivotPoint.position + offset;
    }
}