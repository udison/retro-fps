using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;
    
    [SerializeField] private CharacterController charController;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    
    private Vector3 movementInput = Vector3.zero;
    private Vector3 motion = Vector3.zero;
    private bool wasGroundedLastFrame;
    private bool isGrounded;

    void FixedUpdate()
    {
        GroundCheck();
        ApplyGravity();
        Move();
    }
    
    public void MoveInput(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        
        movementInput.x = input.x;
        movementInput.z = input.y;
    }

    public void JumpInput(InputAction.CallbackContext value)
    {
        Jump();
    }

    private void GroundCheck()
    {
        wasGroundedLastFrame = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void ApplyGravity()
    {
        if (isGrounded && motion.y < 0)
        {
            motion.y = -5f;
            return;
        }
        else if (wasGroundedLastFrame)
        {
            motion.y = 0f;
        }
        
        motion.y -= gravity * Time.fixedDeltaTime;
    }

    private void Move()
    {
        motion = (transform.forward * movementInput.z + transform.right * movementInput.x) * moveSpeed + new Vector3(0, motion.y, 0);
        charController.Move(motion * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            motion.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
