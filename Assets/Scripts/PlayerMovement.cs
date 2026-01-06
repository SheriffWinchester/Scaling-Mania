using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 move;
    GameObject _camera;
    Camera MainCamera;
    public GrapplingGun grapplingGun;
    public float jumpForce = 5f;
    public float jumpDelay = 3f;
    private float jumpTimer = 4f;

    float horizontalInput;

    public float movementSpeed = 150f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grapplingGun = GetComponent<GrapplingGun>();
        // Initialize the jump timer to the jump delay to allow immediate jump
        jumpTimer = jumpDelay;
    }

    void Update()
    {
        // Update the jump timer
        jumpTimer += Time.deltaTime;
        Debug.Log("Jump Timer: " + jumpTimer);
    }
    void FixedUpdate()
    {
        // Might be wrong macros, CHECK THIS!!!!
#if UNITY_ANDROID || UNITY_IOS
        // Use accelerometer for mobile devices
        horizontalInput = Input.acceleration.x;
#else
        // Use keyboard for other platforms
        horizontalInput = Input.GetAxis("Horizontal");
#endif

        if (grapplingGun.m_springJoint2D.enabled)
        {
            // Get the horizontal input (left or right)
            //float horizontalInput = Input.GetAxis("Horizontal");

            // Move left or right
            rb.AddForce(new Vector2((horizontalInput * movementSpeed) * Time.fixedDeltaTime, 0));
            Debug.Log("Moving: " + horizontalInput);
        }
        if (jumpTimer >= jumpDelay)
        {
            // Touch (Mobile)
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began && touch.position.y <= Screen.height * 0.25f)
                {
                    Jump();
                    jumpTimer = 0f;
                    break;
                }
            }
        }
    }

    public void Jump()
    {
        // Implement the jump logic here
        // For example, applying an upward force to the player's Rigidbody2D
        if (rb != null)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Adjust the force as needed
        }
    }
    public void JumpButton()
    {
        if (jumpTimer >= jumpDelay)
        {
            if (rb != null)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Adjust the force as needed
            }
            jumpTimer = 0f;
        }
    }
}