using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform target;  // The character to follow (Capsule)
    public float height = 10f; // Height of the camera above the player
    public float smoothSpeed = 0.125f; // How smoothly the camera moves

    private Vector3 currentVelocity; // Used for smooth damping

    void LateUpdate()
    {
        // Set the desired position to the target's position, but with the specified height
        Vector3 desiredPosition = new Vector3(target.position.x, height, target.position.z);
        
        // Smoothly move the camera towards the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // Optionally, make sure the camera always looks at the character
        transform.LookAt(target);
    }
}

