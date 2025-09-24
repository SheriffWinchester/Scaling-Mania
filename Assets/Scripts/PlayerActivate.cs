using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerActivate : MonoBehaviour
{
    GrapplingRope grapplingRope;
    GrapplingGun grapplingGun;
    PlayerMovement playerMovement;
    void Start()
    {
        grapplingRope = GetComponent<GrapplingRope>();
        grapplingGun = GetComponent<GrapplingGun>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivatePlayer()
    {
        grapplingRope.enabled = true;
        grapplingGun.enabled = true;
        playerMovement.enabled = true;
    }
    public void DeactivatePlayer()
    {
        grapplingRope.enabled = false;
        grapplingGun.enabled = false;
        playerMovement.enabled = false;
    }
} 
