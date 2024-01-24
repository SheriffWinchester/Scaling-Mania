using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 move;
    GameObject _camera;
    Camera MainCamera;
    public GrapplingGun grapplingGun;

    public float movementSpeed = 150f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grapplingGun = GetComponent<GrapplingGun>();
    }

    void Update()
    {   
        //HookSwing();
        if (grapplingGun.m_springJoint2D.enabled)
        {
            // Get the horizontal input (left or right)
            float horizontalInput = Input.GetAxis("Horizontal");

            // Apply a force to the Rigidbody2D in the direction of the input
            rb.AddForce(new Vector2((horizontalInput * movementSpeed) * Time.deltaTime, 0));
        }
    }
    void HookSwing()
    {
        
    }
}
