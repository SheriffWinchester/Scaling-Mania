using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snow : MonoBehaviour
{
    public RectTransform uiSprite;
    public Canvas canvas;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        uiSprite = canvas.transform.Find("Snow").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.y > 1) //Check only Y axis, it is sufficient. > 1 is out of the upper bound
        {
            Debug.Log("Viewport position: " + viewportPosition);
            canvas.gameObject.SetActive(true);
            float yPosition = Screen.height * 0.3f; // 80% of the screen height to place the UI element
            Vector2 position = Camera.main.WorldToScreenPoint(transform.position);
            position = canvas.transform.InverseTransformPoint(position);

            //uiSprite.position = new Vector2(position.x, yPosition);
            uiSprite.anchoredPosition = new Vector2(position.x, yPosition);
            Debug.Log("Screen height: " + Screen.height);
        } 
        else
        {
            canvas.gameObject.SetActive(false);
        }
        if (viewportPosition.y < 0)//If out of the lower bound, destroy the object
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player collided with snow");
            // Get the player's layer
            int playerLayer = player.layer;

            // Get the grappable layer
            int grappableLayer = LayerMask.NameToLayer("Grappable");

            // Disable the collision between the player's layer and the grappable layer
            Physics2D.IgnoreLayerCollision(playerLayer, grappableLayer, true);
        }
    }
}
