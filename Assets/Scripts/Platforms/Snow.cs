using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snow : MonoBehaviour
{
    public RectTransform uiSprite;
    public Canvas canvas;
    public GameObject player;
    public GameObject snowPrefab;
    public GrapplingGun grapplingGun;
    public GrapplingRope grappleRope;
    int playerLayer;
    int grappableLayer;
    void Start()
    {
        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        uiSprite = canvas.transform.Find("Image").GetComponent<RectTransform>();
        grapplingGun = player.GetComponent<GrapplingGun>();
        grappleRope = player.GetComponent<GrapplingRope>();

        // StartCoroutine(DestroySnowAfterDelay(10f));
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.y > 1) //Check only Y axis, it is sufficient. > 1 is out of the upper bound
        {
            Debug.Log("Viewport position: " + viewportPosition);
            uiSprite.gameObject.SetActive(true);
            float yPosition = Screen.height * 0.3f; // 80% of the screen height to place the UI element
            Vector2 position = Camera.main.WorldToViewportPoint(transform.position);
            position = canvas.transform.InverseTransformPoint(position);

            //position.x *= Screen.width;
            // position.y = yPosition;

            //uiSprite.position = new Vector2(position.x, yPosition);
            uiSprite.anchoredPosition = new Vector2(position.x, yPosition);
            Debug.Log("Screen height: " + Screen.height);
        } 
        else
        {
            uiSprite.gameObject.SetActive(false);
        }
        if (viewportPosition.y < 0)//If out of the lower bound, destroy the object
        {
            Destroy(gameObject);
        }
    }
    //When the player collides with the snow, disable the collision between the player and the grappable layer
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player collided with snow");
            
            //grappleRope = collision.gameObject.GetComponent<GrapplingRope>();
            grappleRope.enabled = false;
            //grapplingGun = collision.gameObject.GetComponent<GrapplingGun>();
            grapplingGun.m_springJoint2D.enabled = false;
            // Get the player's layer
            playerLayer = player.layer;
            // Get the grappable layer
            grappableLayer  = LayerMask.NameToLayer("Grappable");
            // Disable the collision between the player's layer and the grappable layer
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            Singleton.instance.snowDisabledCollision = true;

            Vector2 contactPoint = collision.GetContact(0).point;

            // Create two new snow objects
            GameObject snow1 = Instantiate(snowPrefab, transform.position, Quaternion.identity);
            GameObject snow2 = Instantiate(snowPrefab, transform.position, Quaternion.identity);

            // Set the positions of the new snow objects to simulate a split
            snow1.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            snow2.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);

            // Destroy the original snow object
            Destroy(gameObject);
        }
    }
    // IEnumerator DestroySnowAfterDelay(float delay)
    // {
    //     // Wait for the specified delay
    //     yield return new WaitForSeconds(delay);

    //     // Destroy the snow
    //     Destroy(gameObject);
    // }
}
