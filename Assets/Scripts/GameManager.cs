using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        Screen.SetResolution(1440, 3088, false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player's Y position is less than 0 in viewport coordinates
        if (player != null && Camera.main.WorldToViewportPoint(player.transform.position).y < 0)
        {
            // Reload the current scene to restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}