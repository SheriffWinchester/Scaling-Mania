using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public GameObject player;
    
    int playerLayer;
    int grappableLayer;

    void Start()
    {
        player = GameObject.Find("Player");
        //Screen.SetResolution(1440, 3088, false);
        playerLayer = player.layer;
        // Get the grappable layer
        grappableLayer  = LayerMask.NameToLayer("Grappable");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player's Y position is less than 0 in viewport coordinates
        if (player != null && Camera.main.WorldToViewportPoint(player.transform.position).y < 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, grappableLayer, false);//Enable collision of the player if snow collided with the player
            // Reload the current scene to restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}