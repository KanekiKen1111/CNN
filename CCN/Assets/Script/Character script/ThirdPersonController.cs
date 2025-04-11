using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public Transform cameraTarget; // empty GameObject above player
    public Transform cameraTransform; // your Main Camera
    public float mouseSensitivity = 2f;
    public float distanceFromPlayer = 5f;
    public float height = 2f;
    public float pitchMin = -30f;
    public float pitchMax = 60f;
    public float cameraSmoothTime = 0.05f;

    private CharacterController controller;
    private float verticalVelocity;
    private float yaw;
    private float pitch;
    private Vector3 cameraVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleCameraRotation();
        HandleMovement();
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // Move direction relative to camera yaw
            Vector3 moveDir = Quaternion.Euler(0, yaw, 0) * inputDir;

            // Rotate character to face movement direction
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);

            // Apply gravity
            if (controller.isGrounded)
                verticalVelocity = 0f;
            else
                verticalVelocity += gravity * Time.deltaTime;

            moveDir.y = verticalVelocity;

            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }
    }

    void HandleCameraRotation()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
    }

    void UpdateCameraPosition()
    {
        if (!cameraTarget) return;

        Vector3 targetPosition = cameraTarget.position - 
            (Quaternion.Euler(pitch, yaw, 0) * Vector3.forward * distanceFromPlayer) + 
            new Vector3(0, height, 0);

        cameraTransform.position = Vector3.SmoothDamp(
            cameraTransform.position,
            targetPosition,
            ref cameraVelocity,
            cameraSmoothTime
        );

        cameraTransform.LookAt(cameraTarget);
    }
}







