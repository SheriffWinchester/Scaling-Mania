using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform2 : MonoBehaviour
{
    GrapplingGun grapplingGun;
    GrapplingRope grapplingRope;
    SpringJoint2D m_springJoint2D;
    Rigidbody2D rb;
    bool shakes = false;
    
    void Start()
    {
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        grapplingGun = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject.GetComponent<GrapplingGun>();
        grapplingRope = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject.GetComponent<GrapplingRope>();
        m_springJoint2D = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject.GetComponent<SpringJoint2D>();
    }
    void Update()
    {
        if (shakes == false)
        {
            Fall();
        }
    }
    IEnumerator DoShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            
            float xPos = originalPos.x + x;
            float yPos = originalPos.y + y; 
            transform.localPosition = new Vector3(xPos, yPos, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
        shakes = true;
    }

    void Fall()
    {
        //When the player is grappling to this platform
        GameObject grapplingTarget = grapplingGun.GetCurrentTarget();
        if (grapplingTarget == this.gameObject && grapplingRope.isGrappling == true)
        {
            Debug.Log("Fall script works");
            //grapplingRope.enabled = false;
            //m_springJoint2D.enabled = false;

            Shake(0.5f, 0.05f); // Shake for 0.5 seconds with a magnitude of 0.2
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
