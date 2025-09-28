using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // Handles horizontal movement
        horizontal = Input.GetAxisRaw("Horizontal");

        // Handles vertcal movement
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // the movement of the player is updated
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

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
}