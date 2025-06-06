// Handles first-person mouse look logic with sensitivity and activation control.
// Applies vertical rotation to the camera and horizontal rotation to the player.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MouseLooking : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform cameraTransform;
    private float xRotation = 0f;

    private bool _isActive;

// Enables mouse look and sets sensitivity if unset.
    public void StartGameplay()
    {
        _isActive = true;
        
        SetSensitivity();
    }
    
// Disables mouse look input.
    public void FinishGameplay()
    {
        _isActive = false;
    }
    
// Custom update method, only processes input if active.
    public void CustomUpdate()
    {
        if(!_isActive)
            return;
        
        SetMovementInputs();
    }
    
// Assigns default sensitivity if none is set.
    private void SetSensitivity()
    {
        if (mouseSensitivity == 0)
        {
            mouseSensitivity = 200f;
        }
    }
    
// Reads mouse delta, applies vertical rotation to camera and horizontal rotation to player with clamping.
    private void SetMovementInputs()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -65f, 65f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}