using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    public Transform targetObject;

    public Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = targetObject.position + cameraOffset;
        transform.position = newPosition;
    }
}