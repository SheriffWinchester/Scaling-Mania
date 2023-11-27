using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    Transform player;
    SpringJoint2D springJoint2D;
    public static bool needReset = false;
    public Vector2 hookPosReset;
    Vector2 worldPosReset;
    GrapplingHook grapplingHook;

    void Start()
    {
        player = this.gameObject.transform.GetChild(0);
        springJoint2D = player.GetComponent<SpringJoint2D>();
        Debug.Log(player.name);
        grapplingHook = GameObject.Find("Player").GetComponent<GrapplingHook>();
    }
    void Update()
    {
        //Debug.Log(grapplingHook.hitPosition);
        //Debug.Log(player.localPosition);
        if (player.localPosition.y > 17 && needReset == false)//When reached the border - reset the world to the default position
        {
            needReset = true;

            springJoint2D.enabled = false;
            //Debug.Log(springJoint2D.enabled);
            worldPosReset = new Vector2(-7f, -30f);
            this.transform.position = worldPosReset; 
            //Debug.Log("GrapHook: " + grapplingHook.hitPosition);
            hookPosReset = grapplingHook.hitPosition + worldPosReset; //Calculate position of the hook after the reset
            //Debug.Log("Calculated: " + hookPosReset);
            //springJoint2D.connectedAnchor = hookPosReset;

            Debug.Log(springJoint2D.enabled);
            //Debug.Log(transform.localPosition);
        }
        //needReset = false;
    }
    
}
