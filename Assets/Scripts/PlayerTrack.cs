using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrack : MonoBehaviour
{
    float previousYPosition;
    public GameObject player;
    public GrapplingGun grapplingGun;
    public GrapplingRope grappleRope;
    int playerLayer;
    int grappableLayer;

    void Start()
    {
        grapplingGun = GetComponent<GrapplingGun>();
        grappleRope = GetComponent<GrapplingRope>();
        // Store the initial Y position of the player
        previousYPosition = transform.position.y;

        playerLayer = player.layer;
        // Get the grappable layer
        grappableLayer  = LayerMask.NameToLayer("Grappable");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved by 1 unit in the Y axis
        if (transform.position.y - previousYPosition >= 1)
        {
            // Increase the score
            Singleton.instance.mainScore += 1;

            // Update the previous Y position
            previousYPosition = transform.position.y;
            Debug.Log("Score: " + Singleton.instance.mainScore);
        }
        Debug.Log("Script enabled: " + Singleton.instance.snowDisabledCollision);
        if (Singleton.instance.snowDisabledCollision == true) //If the player collided with the snow - grapple to something and it will enable the collision again
        {
            if (grapplingGun.m_springJoint2D.enabled == true)
            {
                Debug.Log("Script enabled: " + Singleton.instance.snowDisabledCollision);
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                Debug.Log("Layers");
                Singleton.instance.snowDisabledCollision = false;
            }
        }
    }
}
