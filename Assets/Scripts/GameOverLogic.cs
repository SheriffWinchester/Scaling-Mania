using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameOverLogic : MonoBehaviour
{
    public MenuController menuController;
    PlayerActivate playerActivate;
    public GameObject player;
    private bool gameOverTriggered = false;

    void Start()
    {
        menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        //playerActivate = GameObject.Find("TrackManager").transform.Find("GamePlayer").GetComponent<PlayerActivate>();
        player = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject;
        //Deactivate player controls
        //playerActivate.DeactivatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player is: " + player);
        // Check if the player's Y position is less than 0 in viewport coordinates
        if (!gameOverTriggered && player != null && player.activeInHierarchy && Camera.main.WorldToViewportPoint(player.transform.position).y < 0)
        {
            gameOverTriggered = true; // Prevent further execution
            Debug.Log("Player fell down, game over");
            menuController.SetActiveState(MenuController.MenuState.GameOver);
            player.SetActive(false); //Deactivate player object
        }
        gameOverTriggered = false; // Reset for next frame check (if needed)
    }
}