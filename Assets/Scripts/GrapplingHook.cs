using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    Camera mainCamera;
    //bool grappleEnabled = false;
    bool grappleAnchored = false;
    public bool hookResetEnabled = false;
    public Vector2 hitPosition;
    Vector2 mousePos;
    SpringJoint2D springJoint2D;
    TrackManager trackManager;
    RaycastHit2D hit;
    
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

                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);   
                Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
                hit = Physics2D.Linecast(transform.position, mousePos);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.tag);

                    if (hit.collider.tag == "Ground") 
                    {
                        hitPosition = hit.point;

                        springJoint2D.enabled = true;
                        springJoint2D.connectedAnchor = hitPosition;
                    }
                }
                
            }
            
            if (hookResetEnabled == false && hit.collider != null) 
            {
                Debug.DrawLine(transform.position, hitPosition);
            } 
            if (hookResetEnabled == true && hit.collider != null) //Activate the hook's position accordingly after the reset (TrackManager.cs) 
            {
                Debug.DrawLine(transform.position, trackManager.hookPosReset);
            }
            if (hit.collider == null)
            {
                Debug.DrawLine(transform.position, mousePos);
            }
            
        }
        else {
            springJoint2D.enabled = false;
            grappleAnchored = false;
            hookResetEnabled = false;
        }
    }
    void Hook()
    {
        
    }
}
