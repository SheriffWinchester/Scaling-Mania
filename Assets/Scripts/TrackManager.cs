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
        grapplingHook = GameObject.Find("Player").GetComponent<GrapplingHook>();
        springJoint2D = player.GetComponent<SpringJoint2D>();
        //Debug.Log(player.name);
    }
    void Update()
    {
        ResetWorldPosition();
        needReset = false;
    }
    void ResetWorldPosition()
    {
        if (player.position.y > 17 && needReset == false) //When reached the border - reset the world to the default position
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
            //Debug.Log("Global: " + player.position);
            Debug.Log("Local: " + player.localPosition);
            //Debug.Log(transform.localPosition);
            grapplingHook.hookResetEnabled = true; //Activate the hook's position accordingly after the reset
        }
    }
    
}
