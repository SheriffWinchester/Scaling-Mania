using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("Sound Settings")]
    public bool soundEnabled = true;
    
    [Header("UI Elements")]
    public Image soundButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    
    void Start()
    {
        // Initialize sound state and button sprite
        UpdateSoundState();
        UpdateButtonSprite();
    }
    
    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        if (soundEnabled)
        {
            soundEnabled = false;
            Debug.Log("Sound is currently ON. Turning it OFF.");
        }
        else
        {
            Debug.Log("Sound is currently OFF. Turning it ON.");
        }
        UpdateSoundState();
        UpdateButtonSprite();
    }
    
    private void UpdateSoundState()
    {
        if (soundEnabled)
        {
            AudioListener.volume = 1f; // Enable all sounds
        }
        else
        {
            AudioListener.volume = 0f; // Mute all sounds
        }
    }
    
    private void UpdateButtonSprite()
    {
        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = soundEnabled ? soundOnSprite : soundOffSprite;
        }
    }
}