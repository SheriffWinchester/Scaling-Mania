using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : _GameState
{
    //Specific for this state
    public GameObject player;
    
    public override void InitState(GameStateController gameStateController)
    {
        base.InitState(gameStateController);

        state = GameStateController.GameState.GameOver;
        Debug.Log("State: " + state);
    }
}
