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
        grappableLayer  = LayerMask.NameToLayer("Grappable");
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously search for the player until it is found
        if (player == null)
        {
            player = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject;
            if (player != null)
            {
                playerLayer = player.layer;
                Debug.Log("Player found and layers set");
            }
        }
        // Check if the player's Y position is less than 0 in viewport coordinates
        // if (player != null && Camera.main.WorldToViewportPoint(player.transform.position).y < 0)
        // {
        //     //Physics2D.IgnoreLayerCollision(playerLayer, grappableLayer, false);//Enable collision of the player if snow collided with the player
        //     // Reload the current scene to restart the game
        //     Debug.Log("Restart the scene");
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }
    }
}