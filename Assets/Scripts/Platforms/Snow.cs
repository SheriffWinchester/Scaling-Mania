using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snow : MonoBehaviour
{
    public RectTransform uiSprite1, uiSprite2, uiSprite3, uiSprite4, uiSprite5;
    public Canvas canvas;
    public GameObject player;
    public GameObject snowPrefab;
    public GrapplingGun grapplingGun;
    public GrapplingRope grappleRope;
    int playerLayer;
    int grappableLayer;
    string snowNumber;
    void Start()
    {
        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        uiSprite1 = canvas.transform.Find("Interface/Exclamation Mark 1").GetComponent<RectTransform>();
        uiSprite2 = canvas.transform.Find("Interface/Exclamation Mark 2").GetComponent<RectTransform>();
        uiSprite3 = canvas.transform.Find("Interface/Exclamation Mark 3").GetComponent<RectTransform>();
        uiSprite4 = canvas.transform.Find("Interface/Exclamation Mark 4").GetComponent<RectTransform>();
        uiSprite5 = canvas.transform.Find("Interface/Exclamation Mark 5").GetComponent<RectTransform>();
        grapplingGun = player.GetComponent<GrapplingGun>();
        grappleRope = player.GetComponent<GrapplingRope>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.y > 1) //Check only Y axis, it is sufficient. > 1 is out of the upper bound
        {
            Debug.Log("Viewport position: " + viewportPosition);
            snowNumber = gameObject.name.Substring(gameObject.name.Length - 1);

            float yPosition = Screen.height * 0.3f; // 80% of the screen height to place the UI element
            Vector2 position = Camera.main.WorldToViewportPoint(transform.position);
            position.x = (position.x - 0.5f) * Screen.width;
            
                    // Check the snow number and activate the appropriate uiSprite
            switch (snowNumber)
            {
                case "1":
                    uiSprite1.gameObject.SetActive(true);
                    uiSprite1.anchoredPosition = new Vector2(position.x, yPosition);
                    break;
                case "2":
                    uiSprite2.gameObject.SetActive(true);
                    uiSprite2.anchoredPosition = new Vector2(position.x, yPosition);
                    break;
                case "3":
                    uiSprite3.gameObject.SetActive(true);
                    uiSprite3.anchoredPosition = new Vector2(position.x, yPosition);
                    break;
                case "4":
                    uiSprite4.gameObject.SetActive(true);
                    uiSprite4.anchoredPosition = new Vector2(position.x, yPosition);
                    break;
                case "5":
                    uiSprite5.gameObject.SetActive(true);
                    uiSprite5.anchoredPosition = new Vector2(position.x, yPosition);
                    break;
                default:
                    Debug.LogWarning("Unknown snow number: " + snowNumber);
                    break;
            }
            //uiSprite1.anchoredPosition = new Vector2(position.x, yPosition);
            Debug.Log("Screen height: " + Screen.height);
        } 
        else
        {
            // Deactivate all uiSprites if the snow is not out of the upper bound
            uiSprite1.gameObject.SetActive(false);
            uiSprite2.gameObject.SetActive(false);
            uiSprite3.gameObject.SetActive(false);
            uiSprite4.gameObject.SetActive(false);
            uiSprite5.gameObject.SetActive(false);
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

            grappleRope.enabled = false;
            grapplingGun.m_springJoint2D.enabled = false;

            // Get the player's layer
            playerLayer = player.layer;
            // Get the grappable layer
            grappableLayer  = LayerMask.NameToLayer("Grappable");
            // Disable the collision between the player's layer and the grappable layer
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            Singleton.instance.snowDisabledCollision = true;

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
    void AssignExclamationMark(Vector2 position, float yPosition)
    {
        // Check the snow number and activate the appropriate uiSprite
        switch (snowNumber)
        {
            case "1":
                uiSprite1.gameObject.SetActive(true);
                uiSprite1.anchoredPosition = new Vector2(position.x, yPosition);
                break;
            case "2":
                uiSprite2.gameObject.SetActive(true);
                uiSprite2.anchoredPosition = new Vector2(position.x, yPosition);
                break;
            case "3":
                uiSprite3.gameObject.SetActive(true);
                uiSprite3.anchoredPosition = new Vector2(position.x, yPosition);
                break;
            case "4":
                uiSprite4.gameObject.SetActive(true);
                uiSprite4.anchoredPosition = new Vector2(position.x, yPosition);
                break;
            case "5":
                uiSprite5.gameObject.SetActive(true);
                uiSprite5.anchoredPosition = new Vector2(position.x, yPosition);
                break;
            default:
                Debug.LogWarning("Unknown snow number: " + snowNumber);
                break;
        }
    }
}