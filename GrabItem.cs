using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
public class GrabItem : MonoBehaviour
{
    public Transform grabPoint;
    private GameObject grabobject;
    public float grabR = 2f;
    public LayerMask grabbableLayer;
    

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (Input.GetKeyDown(KeyCode.F))
        {
            /*if(grabobject == null)
            {
                TryGrab();
            }
            else
            {
                DropObject();
            }*/
        }

    }

    private void FixedUpdate()
    {
        if (grabobject != null)
        {
            grabobject.transform.position = Vector2.MoveTowards(
                grabobject.transform.position,
                grabPoint.position,
                10f * Time.deltaTime  // speed multiplier
            );
        }
    }

    private void TryGrab()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, grabR, grabbableLayer);
        if (hit.collider != null)
        {
            Rigidbody2D objRB = hit.collider.GetComponent<Rigidbody2D>();
            if (objRB != null)
            {
                grabobject = hit.collider.gameObject;
                objRB.bodyType = RigidbodyType2D.Kinematic;
                grabobject.transform.position = grabPoint.position;
                grabobject.transform.SetParent(grabPoint);
            }
        }
    }

    private void DropObject()
    {
        if (grabobject == null)
        {
            return;
        }
        Rigidbody2D objRB = grabobject.GetComponent<Rigidbody2D>();
        objRB.bodyType = RigidbodyType2D.Dynamic;
        grabobject.transform.SetParent(null);
        grabobject = null;
    }

}
