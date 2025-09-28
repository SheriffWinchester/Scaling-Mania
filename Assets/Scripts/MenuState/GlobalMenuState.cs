using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMenuState : _State
{
    //Specific for this state
    public override void InitGlobalState(StateController stateController)
    {
        base.InitGlobalState(stateController);

        globalState = StateController.GlobalState.Menu;
        Debug.Log("State: " + globalState);
    }
}
