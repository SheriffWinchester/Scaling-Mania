using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    Camera mainCamera;
    bool grappleEnabled = false;
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
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);   
            Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            var hit = Physics2D.Linecast(transform.position, mousePos);

            grappleEnabled = true;
            springJoint2D.enabled = true;
            springJoint2D.connectedAnchor = mousePos;
            Debug.DrawLine(transform.position, mousePos);

            if (hit.collider != null)
            {
                // _LineRenderer.SetPosition(0, transform.position);
                // _LineRenderer.SetPosition(1, hit.transform.position);
                // _LineRenderer.enabled = true;

                Vector2 _force = transform.position - hit.transform.position;
                Vector2 _normForce = Vector3.Normalize(_force);
                hit.rigidbody.velocity = _normForce * 5.0f;
            } 
            // else 
            // {
            //     _LineRenderer.enabled = false;
            // }
        }
        else {
            springJoint2D.enabled = false;
        }
    }
}
