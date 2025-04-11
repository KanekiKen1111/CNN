using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed of the capsule

    private Vector3 moveDirection;

    void Update()
    {
        // Get input from the user (WASD or Arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down Arrow

        // Create a movement vector based on user input
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Move the capsule
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}

