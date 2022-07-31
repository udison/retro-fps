using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    [SerializeField] private float maxRotation;
    
    private Vector2 swayAxis = Vector2.zero;

    private void Update()
    {
        // Clamp axis magnitude
        swayAxis = Vector2.ClampMagnitude(swayAxis, maxRotation);
        Debug.Log(swayAxis.magnitude);
        
        // Calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-swayAxis.y, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(swayAxis.x, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;
        
        
        // Apply sway
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    public void ReceiveLookInput(InputAction.CallbackContext value)
    {
        swayAxis = value.ReadValue<Vector2>() * swayMultiplier;
    }
}
