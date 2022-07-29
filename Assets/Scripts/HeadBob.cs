using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float frequency = 10.0f;
    [SerializeField, Range(0, 10f)] private float velocityThreshold = 2.0f;

    [SerializeField] private Transform camera;
    [SerializeField] private CharacterController charController;

    private float movementMagnitude = 0f;

    private void Awake()
    {
        charController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        if (!enable) return;

        CheckVelocity();
    }

    private void CheckVelocity()
    {
        movementMagnitude = charController.velocity.magnitude;
        Debug.Log(movementMagnitude);
        if (movementMagnitude < velocityThreshold)
        {
            ResetPosition();
            return;
        };
        
        Bob();
        FocusView();
    }

    private void ResetPosition()
    {
        camera.localPosition = Vector3.Lerp(
            camera.localPosition,
            Vector3.zero,
            1 * Time.deltaTime
        );
    }

    private void Bob()
    {
        float movementMultiplier = Mathf.Clamp(movementMagnitude, .5f, 2.5f);

        Vector3 offset = new Vector3(
            Mathf.Sin(Time.time * frequency * 2) * amplitude,
            Mathf.Cos(Time.time * frequency * 2) * amplitude * 2,
            0
        );

        camera.localPosition = offset;
    }

    private void FocusView()
    {
        camera.LookAt(transform.position + transform.forward * 15f);
    }
}
