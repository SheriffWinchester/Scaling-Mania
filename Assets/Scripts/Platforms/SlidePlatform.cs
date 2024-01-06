using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePlatform : MonoBehaviour
{
    public int randomSide;
    Rigidbody2D rb;
    public float speed = 1f;
    public float coefSpeed = 1f;
    Vector2 viewportPosition;
    void Start()
    {
        randomSide = Random.Range(0, 1);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Slide();
        viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Debug.Log("Viewport position: " + viewportPosition);
        if (viewportPosition.x < 0.00f || viewportPosition.x > 1.00f)
        {
            Debug.Log("Viewport checks " + viewportPosition.x);
            Debug.Log("Random side: " + randomSide);
            // Change direction
            randomSide = 1 - randomSide;
            Slide();
        }
    }
    void Slide()
    {
        if (randomSide == 0) //Slide left
        {
            rb.velocity = new Vector2(-speed * coefSpeed, 0);
        }
        else //Slide right
        {
            rb.velocity = new Vector2(speed * coefSpeed, 0);
        }
    }
    IEnumerator ChangeDirectionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Viewport checks " + viewportPosition.x);
        Debug.Log("Random side: " + randomSide);
        // Change direction
        randomSide = 1 - randomSide;
    }
}
