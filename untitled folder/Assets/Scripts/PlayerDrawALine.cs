using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDrawALine : MonoBehaviour
{
    private LineRenderer lineRend;
    private Vector2 startPos;
    private Vector2 currentPos;
    private float distance;
    private bool isDragging = false;
    private float lineZ;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.enabled = false; // hidden until drag starts
        lineRend.useWorldSpace = true;
        lineZ = transform.position.z;
    }

    void Update()
    {
        // Touch input (mobile)
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                isDragging = true;
                startPos = ScreenToWorldOnZ(t.position);
                lineRend.enabled = true;
                lineRend.SetPosition(0, new Vector3(startPos.x, startPos.y, lineZ));
                lineRend.SetPosition(1, new Vector3(startPos.x, startPos.y, lineZ));
            }
            else if (isDragging && (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary))
            {
                currentPos = ScreenToWorldOnZ(t.position);
                lineRend.SetPosition(0, new Vector3(startPos.x, startPos.y, lineZ));
                lineRend.SetPosition(1, new Vector3(currentPos.x, currentPos.y, lineZ));
                distance = (currentPos - startPos).magnitude;
            }
            else if (isDragging && (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled))
            {
                isDragging = false;
                lineRend.enabled = false; // hide on release
            }

            return; // don’t process mouse if touch is present
        }
#endif
        //Mouse input (editor/PC)
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startPos = ScreenToWorldOnZ(Input.mousePosition);
            lineRend.enabled = true;
            lineRend.SetPosition(0, new Vector3(startPos.x, startPos.y, lineZ));
            lineRend.SetPosition(1, new Vector3(startPos.x, startPos.y, lineZ));
        }
        if (isDragging && Input.GetMouseButton(0))
        {
            var pull = currentPos - startPos;
            var reversedEnd = startPos - pull; 
            currentPos = ScreenToWorldOnZ(Input.mousePosition);
            lineRend.SetPosition(0, new Vector3(startPos.x, startPos.y, lineZ));
            lineRend.SetPosition(1, new Vector3(reversedEnd.x, reversedEnd.y, lineZ));
            distance = pull.magnitude;
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            lineRend.enabled = false;
        }
    }

    private Vector3 ScreenToWorldOnZ(Vector2 screenPos)
    {
        Camera cam = Camera.main;
        if (cam == null) return Vector3.zero;

        // Project onto the line’s Z plane
        float z = Mathf.Abs(lineZ - cam.transform.position.z);
        Vector3 wp = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, z));
        wp.z = lineZ;
        return wp;
    }
} 
