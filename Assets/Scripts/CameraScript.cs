using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera m_mainCamera;
    public float upwardSpeed = 0.5f;
    bool movedCenter = false;
    public GameObject backgroundImage;
    void Start()
    {
        m_mainCamera = Camera.main;
        backgroundImage = GameObject.Find("Panel");
    }
    void Update()
    {
        if (Singleton.instance.gameStarted == true)
        {
            Vector3 screenPos = m_mainCamera.WorldToScreenPoint(transform.position); //Transform world position to the screen coordinates
            if (movedCenter == true)
            {
                m_mainCamera.transform.position += new Vector3(0, upwardSpeed * Time.deltaTime, 0);//Slowly move the camera upwards
            }
            if (screenPos.y >= Screen.height / 2) //If player moves the center of the screen - move the camera
            {
                movedCenter = true;
                m_mainCamera.transform.position = new Vector3(m_mainCamera.transform.position.x, transform.position.y, (transform.position.z - 10));
            }
        }
    }
}
