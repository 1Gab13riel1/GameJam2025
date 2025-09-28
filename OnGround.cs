using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    // A variable that deals with the Rigidbody of an object
    [SerializeField] private Rigidbody2D rb;

    // A variable that deals with the transform of an object
    [SerializeField] private Transform groundCheck;

    // A variable that deals with layer of object or objects
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        // The function IsGrounded() is called so that
        // check for the object being grounded can be
        // done every time
        IsGrounded();
    }

    // A function that checks if the object is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}

