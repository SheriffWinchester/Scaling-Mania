using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResumeButton : MonoBehaviour
{
    public StateController stateController;
    public AudioClip clickClip;
    [Range(0f, 1f)] public float volume = 1f;
    public AudioSource audioSource;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        button.onClick.AddListener(PlayClick);
    }
    public void PressButton()
    {
        // if (stateController.activeGlobalState.globalState == StateController.GlobalState.Menu)
        // {
        //     stateController.SetActiveGlobalState(StateController.GlobalState.Game);
        // }
        stateController.JumpBack();
        //PlayClick();
    }
    public void PlayClick()
    {
        if (clickClip != null) audioSource.PlayOneShot(clickClip, volume);
    }
}