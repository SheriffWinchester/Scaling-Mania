using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    Camera mainCamera;
    bool grappleEnabled = false;
    bool grappleAnchored = false;
    Vector2 hitPosition;
    SpringJoint2D springJoint2D;
    void Start()
    {
        mainCamera = Camera.main;
        springJoint2D = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (grappleAnchored == false)
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);   
                Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
                var hit = Physics2D.Linecast(transform.position, mousePos);
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Ground")
                {
                    //Debug.Log("Hit point: " + hit.point);
                    //Debug.Log(hit.collider.tag);
                    hitPosition = hit.point;
                    grappleEnabled = true;
                    springJoint2D.enabled = true;
                    springJoint2D.connectedAnchor = hitPosition;
                }

                grappleAnchored = true;
            }
            Debug.DrawLine(transform.position, hitPosition);
            

            // if (hit.collider != null)
            // {
            //     // _LineRenderer.SetPosition(0, transform.position);
            //     // _LineRenderer.SetPosition(1, hit.transform.position);
            //     // _LineRenderer.enabled = true;

            //     Vector2 _force = transform.position - hit.transform.position;
            //     Vector2 _normForce = Vector3.Normalize(_force);
            //     hit.rigidbody.velocity = _normForce * 5.0f;
            // } 
            // else 
            // {
            //     _LineRenderer.enabled = false;
            // }
        }
        else {
            springJoint2D.enabled = false;
            grappleAnchored = false;
        }
    }
}
