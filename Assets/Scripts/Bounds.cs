using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    float xPos;
    float yPos;
    public GrapplingRope grappleRope;
    public GrapplingGun grappleGun;
    BoxCollider2D boxCollider;
    Camera mainCamera;
    public float playerVelocity = 0.7f;
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
    void Update() 
    {
        ScaleCameraBounds();
    }

    //Scale the camera's collider bounds to the screen size
    public void ScaleCameraBounds()
    {
        Vector2 topRightCorner = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Vector2 bottomLeftCorner = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        Debug.Log(topRightCorner);
        Debug.Log(bottomLeftCorner);
        Vector2 size = (topRightCorner - bottomLeftCorner) * 2.15f;//Change this value to change correctly the size of the camera's collider bounds
        boxCollider.size = size;
        Debug.Log(size);
    }
    public void InversePosition(Collider2D collider)
    {
        xPos = collider.transform.position.x;
        yPos = collider.transform.position.y;
      
        //When player goes out of bounds, move them to the opposite side of the screen and reduce its velocity
        if (xPos >= 0)//Right side of the screen
        {
            grappleRope.enabled = false;//Disable the rope so it doesn't get stuck on the other side of the screen
            grappleGun.m_springJoint2D.enabled = false;
            //Debug.Log("Works");

            collider.transform.position = new Vector2((xPos * 0) - xPos, yPos);
            Rigidbody2D playerRigidbody = collider.GetComponent<Rigidbody2D>();

            Debug.Log("Velocity: " + playerRigidbody.velocity);
            playerRigidbody.velocity = playerRigidbody.velocity *= playerVelocity;
            Debug.Log("Velocity reduced: " + playerRigidbody.velocity);
        }
        else if (xPos < 0)//Left side of the screen
        {
            grappleRope.enabled = false;
            grappleGun.m_springJoint2D.enabled = false;
            xPos = Mathf.Abs(xPos);//Make xPos positive

            //Debug.Log("Less than zero");
            collider.transform.position = new Vector2((xPos * 0) + xPos, yPos);
            Rigidbody2D playerRigidbody = collider.GetComponent<Rigidbody2D>();

            Debug.Log("Velocity: " + playerRigidbody.velocity);
            playerRigidbody.velocity = playerRigidbody.velocity *= playerVelocity;
            Debug.Log("Velocity reduced: " + playerRigidbody.velocity);
        }
    }
}