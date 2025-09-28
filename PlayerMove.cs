using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* The variables are set to private so that
    they can only be used on this scrpit. */

    // A variable deals with horizontal movement
    private float horizontal;

    // A variable deals with move speed and is set to 5.0f
    private float moveSpeed = 5.0f;

    // A Variable deals with jump force and is set to 15.0f
    private float jumpForce = 8.0f;


    /* The variables are set to private so that
    they can only be used on this scrpit but are 
    also set to SerializeField so that objects and other stuff
    can obtained and be used for this script. */

    // A variable that deals with the Rigidbody of an object
    [SerializeField] private Rigidbody2D rb;

    // A variable that deals with the transform of an object
    [SerializeField] private Transform groundCheck;

    // A variable that deals with layer of object or objects
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Handles horizontal movement
        horizontal = Input.GetAxis("Horizontal");

        // Handles Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }


    }

    private void FixedUpdate()
    {
        // the movement of the player is updated
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    // A function that checks if the player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

}