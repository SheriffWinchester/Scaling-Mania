using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform1 : MonoBehaviour
{
    GrapplingGun grapplingGun;
    GrapplingRope grapplingRope;
    SpringJoint2D m_springJoint2D;
    Rigidbody2D rb;
    
    void Start()
    {
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        grapplingGun = GameObject.Find("Player").GetComponent<GrapplingGun>();
        grapplingRope = GameObject.Find("Player").GetComponent<GrapplingRope>();
        m_springJoint2D = GameObject.Find("Player").GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
    }
    void Fall()
    {
        GameObject grapplingTarget = grapplingGun.GetCurrentTarget();
        if (grapplingTarget == this.gameObject && grapplingRope.isGrappling == true)
        {
            Debug.Log("Fall script works");
            rb.bodyType = RigidbodyType2D.Dynamic;
            grapplingRope.enabled = false;
            m_springJoint2D.enabled = false;
        }
    }
}
