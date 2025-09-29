using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    /* The variables are set to private so that
    they can only be used on this scrpit. */

    // A variable deals with horizontal movement
    private float horizontal;

    // A variable deals with vertical movement
    private float vertical;

    // A variable deals with move speed and is set to 5.0f
    private float moveSpeed = 5.0f;

    // A Variable deals with jump force and is set to 15.0f
    private float jumpForce = 8.0f;

    // A Variable deals with deciding if an object is a ladder
    private bool isLadder;
    // A Variable deals with deciding if the player is climbing
    private bool isClimbing;

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
        // Handles Vertical movement
        vertical = Input.GetAxis("Vertical");

        // Handles Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // if the object is a Ladder and movement is greater than 1,
        // then is climbing is set to true
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }


    }

    private void FixedUpdate()
    {
        // the movement of the player is updated
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        // if the player is Climbing, then the gravity is set to 0 and the player
        // is able to move up. If not, then gravity is set to 4
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * moveSpeed);
            player.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            rb.gravityScale = 4f;
            player.GetComponent<Collider2D>().enabled = false;
        }
    }

    // A function that checks if the player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // a function that deals with the player and the ladder colliding
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the object the player collides with has the tag "Ladder",
        // then isLadder is set to true
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    // a function that deals with the player and the ladder after colliding
    private void OnTriggerExit2D(Collider2D collision)
    {
        // if the object the player collides with has the tag "Ladder",
        // then isLadder is set to false and isClimbing is set to false
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

}
