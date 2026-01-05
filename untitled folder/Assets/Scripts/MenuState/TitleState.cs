using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleState: _State
{
    //Specific for this state
    public override void InitGlobalState(StateController stateController)
    {
        base.InitGlobalState(stateController);

        globalState = StateController.GlobalState.Title;
        Debug.Log("State: " + globalState);
    }

}
