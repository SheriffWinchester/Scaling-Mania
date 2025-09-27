using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : _GameState
{
    //Specific for this state
    public override void InitState(GameStateController gameStateController)
    {
        base.InitState(gameStateController);

        state = GameStateController.GameState.Game;
        Debug.Log("State: " + state);
    }

}
