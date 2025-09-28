using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : _State
{
    //Specific for this state
    public override void InitMenuState(StateController stateController)
    {
        base.InitMenuState(stateController);

        state = StateController.MenuState.SettingsMenu;
        Debug.Log("State: " + state);
    }

}
