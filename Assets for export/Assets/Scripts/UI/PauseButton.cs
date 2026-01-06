using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public StateController stateController;
    public void PressButton()
    {
        stateController.SetActiveGlobalState(StateController.GlobalState.Menu);
        stateController.SetActiveMenuState(StateController.MenuState.PauseMenu);
    }
}