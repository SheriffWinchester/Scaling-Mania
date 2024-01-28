using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    public GameObject winMenu;
    public GameObject scoreMenu;
    public GameObject mainInterface;
    // public TextMeshProUGUI scoreWinText;
    // public TextMeshProUGUI scoreText;

    public GameObject pauseMenu;
    public GameObject tutorialMenu;
    public GameObject tutorialMenu2;
    bool gameIsPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameIsPaused = true;
    }
    void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameIsPaused = false;
    }
}
