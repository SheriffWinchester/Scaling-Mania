using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResumeButton : MonoBehaviour
{
    public StateController stateController;
    public void PressButton()
    {
        // if (stateController.activeGlobalState.globalState == StateController.GlobalState.Menu)
        // {
        //     stateController.SetActiveGlobalState(StateController.GlobalState.Game);
        // }
        stateController.JumpBack();
    }
}