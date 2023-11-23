using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 move;
    GameObject _camera;
    Camera MainCamera;

    public float movementSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
    }
    void Update()
    {   
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 playerPosition = new Vector2(horizontal, vertical);
        rb.MovePosition(playerPosition * movementSpeed * Time.deltaTime);
        // rb.velocity = new Vector2(move.x * movementSpeed, move.y * movementSpeed);
        // move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
