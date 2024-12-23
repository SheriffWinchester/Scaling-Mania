using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;
    public GameObject cursor;
    public bool mousePosActive = true;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public GameObject grappleObject;
    [HideInInspector] public Vector2 grappleDistanceVector;
    [HideInInspector] public RaycastHit2D _hit;
    public GameObject childObject;
    public Vector2 distanceVector;

    int layerMaskGrappable = 1 << 8;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        childObject = new GameObject("ChildObject");

    }

    private void Update()
    {
        //Debug.Log("Camera: " + m_camera.WorldToViewportPoint(transform.position));
        DrawCursor();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Gun script 1");
            SetGrapplePoint();
            // Set the position of the childObject to the grapplePoint
            childObject.transform.position = grapplePoint;

            // Set the parent of the childObject to the grappleObject
            childObject.transform.SetParent(grappleObject.transform, true);
            //grapplePoint = grappleObject.transform.InverseTransformPoint(grapplePoint);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Connected body: " + m_springJoint2D.connectedBody.tag);
            if (grappleRope.enabled)
            {
                Debug.Log("Gun script 2");
                RotateGun(grapplePoint, false);
            }
            else
            {
                Debug.Log("Gun script 3");
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
            m_rigidbody.gravityScale = 1;
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosActive)
            {
                RotateGun(mousePos, true);
            }
        }
        Debug.Log("Gun script 5");
        
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            //Subtract 90 from the angle, to point the gun upwards;
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis((angle - 90), Vector3.forward);
        }
    }

    public void SetGrapplePoint()
    {
        Debug.Log("Set Grapple Point");
        distanceVector = new Vector3(0, 1, 0) - gunPivot.position;
        if (Physics2D.Raycast(origin: firePoint.position, direction: distanceVector.normalized, distance: maxDistnace, layerMask: layerMaskGrappable))
        {
            _hit = Physics2D.Raycast(origin: firePoint.position, direction: distanceVector.normalized, distance: maxDistnace, layerMask: layerMaskGrappable);
            Debug.Log(_hit.transform.name);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleObject = _hit.collider.gameObject;
                    Debug.Log("Point on the platform: " + grapplePoint);
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    Debug.Log(grappleRope.enabled);
                    Debug.DrawRay(firePoint.position, distanceVector.normalized * maxDistnace, Color.red, 2f);
                }
            }
            if (_hit == null)
            {
                grapplePoint = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
                Debug.Log("Point in the air: " + grapplePoint);
                grappleObject = null;
                grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                grappleRope.enabled = true;
            }
        }
    }

    public void Grapple()
    {
        //Debug.Log("Grapple point");
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;

            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    //Debug.Log("Grapple point");
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.linearVelocity = Vector2.zero;
                    break;
            }
        }
    }
    public GameObject GetCurrentTarget()
    {
        if (_hit.collider != null)
        {
            return _hit.collider.gameObject;
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

    private void DrawCursor()
    {
         // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = gunPivot.position.z;
        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - gunPivot.position;

         // If the mouse is out of max distance, switch off the cursor
        if (direction.magnitude > maxDistnace)
        {
            cursor.SetActive(false);
            direction = direction.normalized * maxDistnace;
        }
        else
        {
            // If the mouse is within max distance, switch on the cursor and update its position
            cursor.SetActive(true);

            // Limit the distance of the cursor to maxDistance
            direction = mousePosition - gunPivot.position;

            // Update the position of the cursor
            cursor.transform.position = gunPivot.position + direction;
        }
    }
}
