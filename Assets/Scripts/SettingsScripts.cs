using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("Sound Settings")]
    public bool soundEnabled = true;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image soundButtonImage;
    //public Button soundButton; // assign your button in the Inspector
    [Header("Input Type Settings")]
    public Sprite inputClassicSprite;
    public Sprite inputAlternativeSprite;
    public Image inputButtonImage;

    void Start()
    {
        UpdateSoundButtonSprite();
        UpdateInputButtonSprite();

        // Add listener so pressing the button calls ToggleSound
        //soundButton.onClick.AddListener(ToggleSound);
    }

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        UpdateSoundButtonSprite();

        // Here you can also mute/unmute audio globally if needed:
        AudioListener.volume = soundEnabled ? 1f : 0f;
    }
    public void ToggleInputType()
    {
        // Toggle between Classic and Alternative
        Singleton.instance.inputType = (Singleton.instance.inputType == Singleton.InputType.Classic) ?
            Singleton.InputType.Alternative :
            Singleton.InputType.Classic;

        UpdateInputButtonSprite();

        // Update all GrapplingGun components with the new input type
        //UpdateGrapplingGunInputType();
    }


    private void UpdateSoundButtonSprite()
    {
        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = soundEnabled ? soundOnSprite : soundOffSprite;
        }
    }
    private void UpdateInputButtonSprite()
    {
        if (inputButtonImage != null)
        {
            inputButtonImage.sprite = (Singleton.instance.inputType == Singleton.InputType.Classic) ? 
                inputClassicSprite : 
                inputAlternativeSprite;
        }
    }
}