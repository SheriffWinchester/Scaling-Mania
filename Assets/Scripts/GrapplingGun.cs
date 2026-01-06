using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;
    public GrappleDirectionTouchUI grappleDirectionTouchUI;

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
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;
    public GameObject cursor;
    public bool mousePosActive = true;
    public bool isPlayer = true;

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

    public AudioClip clickClip;
    [Range(0f, 1f)] public float volume = 1f;
    public AudioSource audioSource;

    bool isGrappling = false;
    int layerMaskGrappable = 1 << 8;
    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        childObject = new GameObject("ChildObject");
        //grappleDirectionTouchUI = FindObjectOfType<GrappleDirectionTouchUI>();

    }

    private void Update()
    {
        //Debug.Log("Camera: " + m_camera.WorldToViewportPoint(transform.position));
        // Switch between input methods based on InputType
        switch (Singleton.instance.inputType)
        {
            case Singleton.InputType.Classic:
                ClassicInput();
                break;
            case Singleton.InputType.Alternative:
                AlternativeInput();
                break;
        }
    }
    private void ClassicInput()
    {
        Singleton.instance.inputType = Singleton.InputType.Classic;
        if (grappleDirectionTouchUI.enabled == true)
        {
            grappleDirectionTouchUI.enabled = false;
        }
        DrawCursor();
        if (Input.GetKeyDown(KeyCode.Mouse0) && mousePosActive)
        {
            Debug.Log("Gun script 1");
            SetGrapplePoint();
            // Set the position of the childObject to the grapplePoint
            childObject.transform.position = grapplePoint;

            // Set the parent of the childObject to the grappleObject
            childObject.transform.SetParent(grappleObject.transform, true);
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

        if (isPlayer) // When in player mode - switch off this variable so you can rotate gun
        {
            Singleton.instance.playerObjectMenuReady = false;
        }
        Debug.Log("Gun script 5");
    }
    private void AlternativeInput()
    {
        Singleton.instance.inputType = Singleton.InputType.Alternative;
        DrawCursor();

        if (grappleDirectionTouchUI.enabled == false)
        {
            grappleDirectionTouchUI.enabled = true;
        }
        if (grappleDirectionTouchUI.circleOut)
        {
            SetGrapplePoint();

            // Set the position of the childObject to the grapplePoint
            childObject.transform.position = grapplePoint;

            // Set the parent of the childObject to the grappleObject
            if (grappleObject != null)
            {
                childObject.transform.SetParent(grappleObject.transform, true);
                isGrappling = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && isGrappling)
        {
            isGrappling = false;
            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
            m_rigidbody.gravityScale = 1f;
            childObject.transform.SetParent(null, true);
            grappleObject = null;
        }
        // Handle rotation
        if (grappleRope.enabled)
        {
            RotateGun(grapplePoint, false);
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosActive)
            {
                RotateGun(mousePos, true);
            }
        }

        // if (Input.GetKeyDown(KeyCode.Mouse0) && mousePosActive)
        // {
        //     Debug.Log("Gun script 1");
        //     SetGrapplePoint();
        //     // Set the position of the childObject to the grapplePoint
        //     childObject.transform.position = grapplePoint;

        //     // Set the parent of the childObject to the grappleObject
        //     childObject.transform.SetParent(grappleObject.transform, true);

        //     if (Input.GetKeyDown(KeyCode.Mouse0) && mousePosActive)
        //     {
        //         if (!isGrappling)
        //         {
        //             // Try to grapple
        //             Debug.Log("Gun: Try grapple");
        //             SetGrapplePoint();

        //             // Position helper
        //             childObject.transform.position = grapplePoint;
        //             // Parent only if we hit something
        //             if (grappleObject != null)
        //                 childObject.transform.SetParent(grappleObject.transform, true);
        //             else
        //                 childObject.transform.SetParent(null, true);

        //             // Toggle on only if rope actually enabled
        //             if (grappleRope.enabled)
        //             {
        //                 isGrappling = true;
        //             }
        //         }
        //         else
        //         {
        //             // Release grapple
        //             Debug.Log("Gun: Release grapple");
        //             isGrappling = false;
        //             grappleRope.enabled = false;
        //             m_springJoint2D.enabled = false;
        //             m_rigidbody.gravityScale = 1f;
        //             childObject.transform.SetParent(null, true);
        //             grappleObject = null;
        //         }

        //         // Optional aim update on click
        //         if (grappleRope.enabled)
        //         {
        //             RotateGun(grapplePoint, false);
        //         }
        //         else
        //         {
        //             Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        //             RotateGun(mousePos, true);
        //         }

        //         // Optional launch assist while already grappling
        //         if (launchToPoint && grappleRope.isGrappling)
        //         {
        //             if (launchType == LaunchType.Transform_Launch)
        //             {
        //                 Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
        //                 Vector2 targetPos = grapplePoint - firePointDistnace;
        //                 gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
        //             }
        //         }
        //     }
        // }
        // else
        // {
        //     Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        //     if (mousePosActive)
        //     {
        //         RotateGun(mousePos, true);
        //     }
        // }

        if (isPlayer) // When in player mode - switch off this variable so you can rotate gun
        {
            Singleton.instance.playerObjectMenuReady = false;
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

    public void SetGrapplePoint(bool isPlayer = true)
    {
        Debug.Log("Set Grapple Point");
        if (Singleton.instance.playerObjectMenuReady) // For non-player object in the menu
        {
            distanceVector = new Vector3(0, 1, 0) - gunPivot.position;
        }
        else // For player object
        {
            distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        }
        if (Physics2D.Raycast(origin: firePoint.position, direction: distanceVector.normalized, distance: maxDistnace, layerMask: layerMaskGrappable))
        {
            if (Singleton.instance.playerObjectMenuReady) // For non-player object in the menu
            {
                _hit = Physics2D.Raycast(origin: firePoint.position, direction: distanceVector.normalized, distance: maxDistnace, layerMask: layerMaskGrappable);
            }
            if (Singleton.instance.inputType == Singleton.InputType.Alternative && !Singleton.instance.playerObjectMenuReady) // For Alternative input, use direction variable from grappleDirectionTouchUI
            {
                _hit = Physics2D.Raycast(origin: firePoint.position, direction: -grappleDirectionTouchUI.direction, distance: maxDistnace, layerMask: layerMaskGrappable); //Direction variable is reversed, for touch inputs
            }
            if (Singleton.instance.inputType == Singleton.InputType.Classic && !Singleton.instance.playerObjectMenuReady) // For Classic input
            {
                _hit = Physics2D.Raycast(origin: firePoint.position, direction: distanceVector.normalized, distance: maxDistnace, layerMask: layerMaskGrappable);
            }
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
                    if (clickClip != null) audioSource.PlayOneShot(clickClip, volume); // Play sound on successful grapple
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

        // Change the color of the cursor based on its vertical position
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        Camera MainCamera = Camera.main;
        var sr = cursor.GetComponent<SpriteRenderer>();
        Debug.Log("Pixel Height: " + MainCamera.pixelHeight);
        Debug.Log("Mouse Y: " + mousePos.y);
        if (mousePos.y > MainCamera.pixelHeight * 0.25f)
        {
            if (sr != null)
                sr.color = Color.green;
        }
        else
        {
            if (sr != null)
                sr.color = Color.red;
        }
    }
    public void ResetGrappleInput()
    {
        isGrappling = false;

        if (grappleDirectionTouchUI != null)
        {
            grappleDirectionTouchUI.circleOut = false;
            grappleDirectionTouchUI.direction = Vector2.zero;
        }

        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 1f;

        childObject.transform.SetParent(null, true);
        grappleObject = null;
    }
}