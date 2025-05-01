using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CharacterController;
    
    public float movementSpeed = 50f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;
    
    public Transform groundChecker;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isGround;
    private Vector3 velocity;
    void Update()
    {
        GetMovementInputs();
        AddGravity();
        IsGroundedCheck();
        Jump();
    }


    private void GetMovementInputs()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movementDirection = (transform.right * x) + (transform.forward * z);
        CharacterController.Move(movementDirection * movementSpeed * Time.deltaTime);
    }
    
    private void AddGravity()
    {
        velocity.y += gravity*Time.deltaTime;
        CharacterController.Move(velocity * Time.deltaTime);
    }

    private void IsGroundedCheck()
    {
        isGround = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") & isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }    
    }
    
}
