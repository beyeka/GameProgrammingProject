// Camera controller script that handles mouse-based look movement for both player and camera pitch.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float _sensitivityX = 1f, _sensitivityY = 1f;

    void Update()
    {
        HandleLookX();
        HandleLookY();
    }
// Rotates the player object horizontally based on mouse X input.
    private void HandleLookX()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 rotation = player.transform.localEulerAngles;
        rotation.y += mouseX * _sensitivityX;
        player.transform.localEulerAngles = rotation;
    }
// Rotates the camera vertically based on mouse Y input, with clamping to prevent over-rotation.
    private void HandleLookY()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = transform.localEulerAngles;
        rotation.x += mouseY * _sensitivityY;
        rotation.x = (rotation.x > 180) ? rotation.x - 360 : rotation.x;
        rotation.x = Mathf.Clamp(rotation.x, -70, 60);
        transform.localEulerAngles = rotation;
    }
}