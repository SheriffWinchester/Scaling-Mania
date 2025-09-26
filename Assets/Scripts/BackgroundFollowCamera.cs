using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowCamera : MonoBehaviour
{
    public Transform target;  // object to follow
    public Camera cam;
    public Vector3 offset;

    void Start()
    {
        target = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 camPos = cam.transform.position + offset;
        transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
    }
}
