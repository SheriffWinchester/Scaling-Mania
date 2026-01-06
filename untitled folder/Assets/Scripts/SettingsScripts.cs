using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("Sound Settings")]
    public bool soundEnabled = true;
    
    [Header("UI Elements")]
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    //public Button soundButton; // assign your button in the Inspector

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateButtonSprite();

        // Add listener so pressing the button calls ToggleSound
        //soundButton.onClick.AddListener(ToggleSound);
    }

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        UpdateButtonSprite();

        // Here you can also mute/unmute audio globally if needed:
        AudioListener.volume = soundEnabled ? 1f : 0f;
    }

    private void UpdateButtonSprite()
    {
            buttonImage.sprite = soundEnabled ? soundOnSprite : soundOffSprite;
    }
}