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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
    }

    // A function that checks if the player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
