// Handles player movement, jumping, and gravity using CharacterController. Supports gameplay toggling and speed boosts.
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

    private bool _isActive;

    // Runs movement input, gravity, ground check, and jump logic if active.
    public void CustomUpdate()
    {
        if (!_isActive)
            return;

        GetMovementInputs();
        AddGravity();
        IsGroundedCheck();
        Jump();
    }

    // Gets WASD movement input and moves the character.
    private void GetMovementInputs()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movementDirection = (transform.right * x) + (transform.forward * z);
        CharacterController.Move(movementDirection * (Time.deltaTime * movementSpeed));
    }

    // Applies gravity to the vertical velocity and moves the character downward.
    private void AddGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        CharacterController.Move(velocity * Time.deltaTime);
    }

    // Checks if the player is grounded using a sphere check and resets vertical velocity when landing.
    private void IsGroundedCheck()
    {
        isGround = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    // Performs a jump if grounded and jump key is pressed.
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") & isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Temporarily increases movement speed with a coroutine.
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    // Applies the speed multiplier, waits, then reverts.

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        movementSpeed *= multiplier;
        yield return new WaitForSeconds(duration);
        movementSpeed /= multiplier;
    }


    // Enables player movement.
    public void StartGameplay()
    {
        _isActive = true;
    }

    // Disables player movement.
    public void FinishGameplay()
    {
        _isActive = false;
    }
}