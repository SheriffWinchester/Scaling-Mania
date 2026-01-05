using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : _State
{
    //Specific for this state
    public override void InitGlobalState(StateController stateController)
    {
        base.InitGlobalState(stateController);

        globalState = StateController.GlobalState.Game;
        Debug.Log("State: " + globalState);
    }

}
