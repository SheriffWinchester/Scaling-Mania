using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateScript : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 60; // Set the target frame rate to 60 frames per second.
    }
}
