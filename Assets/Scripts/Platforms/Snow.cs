using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snow : MonoBehaviour
{
    public RectTransform uiSprite;
    public Canvas canvas;
    public GameObject player;
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
            Vector2 position = Camera.main.WorldToScreenPoint(transform.position);
            position = canvas.transform.InverseTransformPoint(position);

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

        if (Singleton.instance.snowDisabledCollision == true) //If the player collided with the snow - grapple to something and it will enable the collision again
        {
            if (grapplingGun.m_springJoint2D.enabled == true)
            {
                Debug.Log("Script enabled: " + Singleton.instance.snowDisabledCollision);
                Physics2D.IgnoreLayerCollision(playerLayer, grappableLayer, false);
                Singleton.instance.snowDisabledCollision = false;
            }
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
            Physics2D.IgnoreLayerCollision(playerLayer, grappableLayer, true);
            Singleton.instance.snowDisabledCollision = true;
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
