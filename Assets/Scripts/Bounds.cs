using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    float xPos;
    float yPos;
    public GrapplingRope grappleRope;
    BoxCollider2D boxCollider;
    Camera mainCamera;
    void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player"))
        {
            InversePosition(collider);
        }
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamera = Camera.main;
    }
    void Update() {
        Vector2 topRightCorner = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Vector2 bottomLeftCorner = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        Debug.Log(topRightCorner);
        Debug.Log(bottomLeftCorner);
        Vector2 size = (topRightCorner - bottomLeftCorner) * 2.15f;
        boxCollider.size = size;
        Debug.Log(size);
    }
    public void InversePosition(Collider2D collider)
    {
        //Vector2 planePosition = collider.transform.position;
        xPos = collider.transform.position.x;
        yPos = collider.transform.position.y;
        // Vector3 screenPos = m_mainCamera.WorldToScreenPoint(transform.position); //Transform world position to the screen coordinates
        // if (screenPos.x >= Screen.width) //If player moves the center of the screen - move the camera
        // {
        //     //Debug.Log("Works");
        //     transform.position = new Vector2((xPos * 0) - (xPos + 0.5f), yPos);
        // }
        // else if (screenPos.x <= 0)
        // {
        //     Debug.Log("Works");
        //     transform.position = new Vector2((xPos * 0) + xPos, yPos);
        // }
        if (xPos >= 0)
        {
            grappleRope.enabled = false;
            Debug.Log("Works");
            collider.transform.position = new Vector2((xPos * 0) - xPos, yPos);
            Rigidbody2D playerRigidbody = collider.GetComponent<Rigidbody2D>();
            Debug.Log(playerRigidbody.velocity);
            playerRigidbody.velocity = playerRigidbody.velocity *= 0.5f;
        }
        else if (xPos < 0)
        {
            grappleRope.enabled = false;
            xPos = Mathf.Abs(xPos);
            Debug.Log("Less than zero");
            collider.transform.position = new Vector2((xPos * 0) + xPos, yPos);
            Rigidbody2D playerRigidbody = collider.GetComponent<Rigidbody2D>();
            Debug.Log(playerRigidbody.velocity);
            playerRigidbody.velocity = playerRigidbody.velocity *= 0.5f;
        }
    }
}