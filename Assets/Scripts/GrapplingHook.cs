using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    Camera mainCamera;
    //bool grappleEnabled = false;
    bool grappleAnchored = false;
    bool hookLineEnabled = false;
    public Vector2 hitPosition;
    SpringJoint2D springJoint2D;
    TrackManager trackManager;
    
    void Start()
    {
        mainCamera = Camera.main;
        springJoint2D = GetComponent<SpringJoint2D>();
        trackManager = GameObject.Find("TrackManager").GetComponent<TrackManager>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (grappleAnchored == false) //Executed only once until the action button is released
            {
                grappleAnchored = true;

                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);   
                Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
                var hit = Physics2D.Linecast(transform.position, mousePos);
                Debug.Log(hit.collider.tag);

                if (hit.collider.tag == "Ground") 
                {
                    hitPosition = hit.point;

                    springJoint2D.enabled = true;
                    springJoint2D.connectedAnchor = hitPosition;
                }
            }
            
            if (TrackManager.needReset == false) 
            {
                Debug.DrawLine(transform.position, hitPosition);
            } else {
                Debug.Log("NeedReset: " + TrackManager.needReset);
                Debug.Log("Hook: " + trackManager.hookPosReset);
                Debug.DrawLine(transform.position, trackManager.hookPosReset);
            }
            
        }
        else {
            springJoint2D.enabled = false;
            grappleAnchored = false;
        }
    }
}
