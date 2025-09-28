using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* The variables are set to private so that
    they can only be used on this scrpit. */

    // A variable deals with horizontal movement
    private float horizontal;
    private float vertical;

    // A variable deals with move speed and is set to 5.0f
    private float moveSpeed = 5.0f;

    private Animator animator;
    private SpriteRenderer sr;

    bool onLadder = false;

    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Handles horizontal movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 velocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        if (onLadder)
        {
            // Allow full vertical movement on ladder
            velocity.y = vertical * moveSpeed;
            rb.gravityScale = 0; // disable gravity while on ladder
        }
        else
        {
            rb.gravityScale = 1; // restore gravity off ladder
        }

        rb.linearVelocity = velocity;
    }

    private void FixedUpdate()
    {
        // the movement of the player is updated
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        //animation controls
        if (horizontal != 0)
        {
            animator.speed = 1;

            //flip sprite based on direction
            if (horizontal > 0)
            {
                sr.flipX = false;
            }
            else if (horizontal < 0)
            {
                sr.flipX = true;
            }
        }
        else
        {
            animator.speed = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Rigidbody2D body = col.rigidbody;
        if (body != null && body.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        Rigidbody2D body = col.rigidbody;
        if (body != null && body.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}