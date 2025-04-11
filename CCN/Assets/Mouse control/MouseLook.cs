using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform target;  // The object to look at (e.g., the Capsule)
    public float rotationSpeed = 5f; // Speed of rotation
    public float maxRotationX = 30f; // Max upward rotation (to limit view angle)
    public float minRotationX = -30f; // Min downward rotation (to limit view angle)

    private float rotationX = 0f; // To store the vertical rotation value
    private float rotationY = 0f; // To store the horizontal rotation value

    void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate rotation around Y (horizontal rotation)
        rotationY += mouseX * rotationSpeed;

        // Calculate rotation around X (vertical rotation, clamped to min/max)
        rotationX -= mouseY * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);

        // Apply rotation to the camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}

