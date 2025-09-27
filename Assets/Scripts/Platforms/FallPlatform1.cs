using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform1 : MonoBehaviour
{
    GrapplingGun grapplingGun;
    GrapplingRope grapplingRope;
    SpringJoint2D m_springJoint2D;
    Rigidbody2D rb;
    public GameObject fallPlatformShattered;
    
    void Start()
    {
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        grapplingGun = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject.GetComponent<GrapplingGun>();
        grapplingRope = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject.GetComponent<GrapplingRope>();
        m_springJoint2D = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject.GetComponent<SpringJoint2D>();
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
            // Instantiate shattered platform at current position and rotation
            Instantiate(fallPlatformShattered, transform.position, transform.rotation);
            rb.bodyType = RigidbodyType2D.Dynamic;
            grapplingGun.m_springJoint2D.enabled = false;
            grapplingRope.enabled = false;
            //m_springJoint2D.enabled = false;
            //grapplingGun.grapplePoint = Vector2.zero;
            // Destroy this platform
            Destroy(gameObject);

        }
    }
}
