using System;
using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoSingleton<Movement>
{
    
    private CharacterController controller;
    private Camera cam;
    private float xRotation, yRotation;
    private Vector3 direction, dir;
    private float speedMultiply = 1f;
    private Quaternion rotation;
    
    [Header("Parameters")]
    public float speed;
    public float jumpHeight;
    public float xSensitivity = 10f;
    public float ySensitivity = 10f;
    public float gravity = -9.81f;

    [Header("Feel parameters")] 
    public float camRotaPower = 1f;
    [HideInInspector]public float yVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        direction = transform.forward * dir.y + cam.transform.right * dir.x;
        direction *= speedMultiply;
        direction.y = yVelocity;
        controller.Move(direction * (speed * Time.deltaTime));
        ApplyGravity();
    }

    public void Move(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 mouseMove = context.ReadValue<Vector2>();

        xRotation += mouseMove.x * Time.deltaTime * xSensitivity;
        yRotation -= mouseMove.y * Time.deltaTime * ySensitivity;
        yRotation = Mathf.Clamp(yRotation, -75, 75);
        
        transform.rotation = Quaternion.Euler(0,xRotation,0);
        cam.transform.localRotation = Quaternion.Euler(yRotation,0,0);
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speedMultiply = 2.5f;
        }

        if (context.canceled)
        {
            speedMultiply = 1f;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && context.performed)
        {
            yVelocity = jumpHeight;
        }
    }
    
    private void ApplyGravity()
    {
        if (IsGrounded()) yVelocity = -0.5f;
        else yVelocity += gravity * Time.deltaTime;
    }
    
    public bool IsGrounded() => controller.isGrounded;

}
