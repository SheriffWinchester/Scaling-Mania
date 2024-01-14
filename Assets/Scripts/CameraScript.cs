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
        Vector3 screenPos = m_mainCamera.WorldToScreenPoint(transform.position); //Transform world position to the screen coordinates
        
        if (screenPos.y >= Screen.height/2) //If player moves the center of the screen - move the camera
        {
            m_mainCamera.transform.position = new Vector3(m_mainCamera.transform.position.x, transform.position.y, (transform.position.z - 10));
        }
    }   
}
