using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : _State
{
    //Specific for this state
    public GameObject player;

    public override void InitMenuState(StateController stateController)
    {
        base.InitMenuState(stateController);

        state = StateController.MenuState.GameOver;
        Debug.Log("State: " + state);
    }

    void Start()
    {
        //player = GetComponent<PlayerActivate>();
    }
}
