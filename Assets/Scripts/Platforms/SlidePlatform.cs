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
        randomSide = Random.Range(0, 2);//Random.Range(0, 1) return only 0, so we need to use 2 instead of 1.
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Random side: " + randomSide);

        // RaycastHit2D hitXaxis = Physics2D.BoxCast(transform.position, new Vector2(transform.localScale.x, transform.localScale.y + 0.01f), 0, Vector2.left);
        // RaycastHit2D hitYaxis = Physics2D.BoxCast(transform.position, new Vector2(transform.localScale.x, transform.localScale.y + 0.01f), 0, Vector2.right);
        // if (hitXaxis.collider != null)
        // {
        //     Debug.Log("Hit left");
        // }
        // if (hitYaxis.collider != null)
        // {
        //     Debug.Log("Hit right");
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Slide();
        viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        float halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x + halfWidth;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x - halfWidth;
        Debug.Log("Viewport position: " + viewportPosition);
        if (transform.position.x < leftBorder || transform.position.x > rightBorder)
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
        //Debug.Log("Random side: " + randomSide);
        // Change direction
        randomSide = 1 - randomSide;
    }
}
