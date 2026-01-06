using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [Header("General Refernces:")]
    public GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int percision = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    public bool isGrappling = true;

    bool strightLine = true;
    public Vector2 previousPos;
    private void OnEnable()
    {
        moveTime = 0;
        m_lineRenderer.positionCount = percision;
        waveSize = StartWaveSize;
        strightLine = false;

        LinePointsToFirePoint();

        m_lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < percision; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }
    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if (!strightLine)
        {
            float threshold = 0.01f; // You can adjust the threshold as needed
            if (Mathf.Abs(m_lineRenderer.GetPosition(percision - 1).x - grapplingGun.grapplePoint.x) < threshold)
            {
                strightLine = true;
                Debug.Log("Straight");
            }
            else
            {
                Debug.Log("Grapple script 1");
                DrawRopeWaves();
            }
        }
        else
        {
            if (!isGrappling)
            {
                grapplingGun.Grapple();
                Debug.Log("Grappling");
                isGrappling = true;
            } 
            else
            {
                Debug.Log("Object: " + grapplingGun.grappleObject.name);
                grapplingGun.grapplePoint = grapplingGun.childObject.transform.position;
                grapplingGun.m_springJoint2D.connectedAnchor = grapplingGun.grapplePoint;
                if (grapplingGun.grappleObject.GetComponent<Rigidbody2D>() != null)
                {
                    
                    //grapplingGun.grapplePoint = childObject.transform.position;
                //     Debug.Log("Has rigidbody");
                //     Debug.Log("Has: " + grapplingGun.m_springJoint2D.connectedAnchor);

                //     // Get the current position of the grapple object
                //     Vector2 currentPos = grapplingGun.grappleObject.transform.position;
                //     Debug.Log("Current pos 1a: " + currentPos);

                //     // Calculate the offset
                //     Vector2 offsetPos = currentPos - previousPos;
                //     Debug.Log("Offset pos 1a: " + offsetPos);

                //     // Update the grapple point
                //     grapplingGun.grapplePoint = grapplingGun.grapplePoint - offsetPos;

                //     // Store the current position for the next frame
                //     previousPos = currentPos;
                }
            
                //Vector2 diffVector = grapplingGun.grappleObject.transform.position - grapplingGun.grapplePoint; 
                //grapplingGun.m_springJoint2D.connectedAnchor = grapplingGun.grappleObject.transform.TransformPoint(grapplingGun.grapplePoint);
                // Debug.Log("Position of the platform  " + grapplingGun.grappleObject.transform.position);
                // Debug.Log("Local of the platform  " + grapplingGun.grappleObject.transform.InverseTransformPoint(grapplingGun.grapplePoint));
            }
            if (waveSize > 0)
            {
                Debug.Log("Grapple script 2");
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                //grapplingGun.grapplePoint = (Vector2)grapplingGun.grappleObject.transform.position + grapplingGun.grapplePoint;
                waveSize = 0;

                if (m_lineRenderer.positionCount != 2) { m_lineRenderer.positionCount = 2; }
                Debug.Log("Grapple script 3");
                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = (float)i / ((float)percision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves()
    {
        m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
        m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }
}