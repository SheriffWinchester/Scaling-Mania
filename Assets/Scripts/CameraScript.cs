using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera m_mainCamera;
    void Start()
    {
        m_mainCamera = Camera.main;
    }
    void Update()
    {
        m_mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z - 10)); //Follow the player
    }
}
