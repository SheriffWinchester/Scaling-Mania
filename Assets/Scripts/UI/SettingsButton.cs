using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public StateController stateController;
    public void PressButton()
    {
        if (stateController.activeGlobalState.globalState == StateController.GlobalState.Game)
        {
            Debug.Log("Switching to Menu from Game");
            stateController.SetActiveGlobalState(StateController.GlobalState.Menu);
        }
        stateController.SetActiveMenuState(StateController.MenuState.SettingsMenu);
    }
}