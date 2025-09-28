using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
public class GrabItem : MonoBehaviour
{
    // This variable is used in statements that involve the
    // grab point of the player
    public Transform grabPoint;
    // This variable is used in statements that involve the
    // object the player is grabbing
    private GameObject grabobject;
    // This statemnet is used in statements that involve the
    // distance between the object and grab point
    public float grabR = 2f;
    // This statemnet is used in statements that involve
    // objects who have the LayerMask grabbableLayer
    public LayerMask grabbableLayer;

    // Update is called once per frame
    void Update()
    {
        // if the key pressed is F, then
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            // the item has not been grabbed, then
            if (grabobject == null)
            { 

                // call the function TryGrab() so the player can Grab it.
                TryGrab();
            }
            else
            {
                // call the function DropObject() so the player can drop it
                DropObject();
            }
        }

        // if the object has not been grabbed, then make so that the
        // player will be able to grab it.
        if (grabobject != null)
        {
            grabobject.transform.position = Vector2.MoveTowards(
                grabobject.transform.position,
                grabPoint.position,
                10f * Time.deltaTime  // speed multiplier
            );
        }
    }

    // A function that makes the player grab the object.
    public void TryGrab()
    {
        // this statement performs a raycast on objects that have a LayerMask
        // that falls under grabbableLayer
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, grabR, grabbableLayer);
        hit.collider.enabled = false;
        // this statement checks if the raycast has made contact with an object that has a LayerMask
        // that falls under grabbableLayer
        if (hit.collider != null)
        {
            // This statement gets the rigidbody of the object that been
            // hit by the raycast.
            Rigidbody2D objRB = hit.collider.GetComponent<Rigidbody2D>();

            // statement checks if the object hit by raycast has rigidbody,
            // and if does, then the player will be able to hold the object
            if (objRB != null)
            {
                // This statement makes the object disables the rigidbody of the
                // object and connects the object to the player
                grabobject = hit.collider.gameObject;
                // This makes the object the player is holding
                // not affected by gravity.
                objRB.bodyType = RigidbodyType2D.Kinematic;
                hit.collider.enabled = false;
                // This statement makes the object attched to grab point
                grabobject.transform.position = grabPoint.position;
                // This statement makes the object attched to grab point
                // no matter where the grab point moves
                grabobject.transform.SetParent(grabPoint);
            }
        }
    }

    

    // A function that makes the player drop the object.
    public void DropObject()
    {
        if (grabobject == null) return;
        //This statement makes the object not attched to grab point
        grabobject.transform.SetParent(null);

        grabobject.GetComponent<Collider2D>().enabled = true;

        // this statemnet gets the rigidbody of the object being held
        Rigidbody2D objRB = grabobject.GetComponent<Rigidbody2D>();

        // This makes the object the player is holding
        // affected by gravity.
        objRB.bodyType = RigidbodyType2D.Dynamic;

        // This makes the object fall out of the player's hand
        grabobject = null;
    }

}

