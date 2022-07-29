using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivity = 5;
    public float maxDegreesY = 90;

    public Transform player;
    public PlayerInput input;
    
    private Vector2 lookDirection = Vector2.zero;

    private float cameraYaw;
    private float cameraPitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
    }

    public void LookInput(InputAction.CallbackContext value)
    {
        lookDirection = value.ReadValue<Vector2>();
    }

    private void Look()
    {
        cameraYaw -= lookDirection.y * sensitivity;
        cameraYaw = Mathf.Clamp(cameraYaw, -maxDegreesY, maxDegreesY);
            
        cameraPitch = lookDirection.x * sensitivity;
        
        transform.localRotation = Quaternion.Euler(cameraYaw, 0, 0);
        
        player.Rotate(new Vector3(0, cameraPitch, 0));
    }
}
