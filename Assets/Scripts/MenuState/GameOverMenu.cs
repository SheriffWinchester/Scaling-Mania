using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : _MenuState
{
    //Specific for this state
    public GameObject player;
    
    public override void InitState(MenuController menuController)
    {
        base.InitState(menuController);

        state = MenuController.MenuState.GameOver;
        Debug.Log("State: " + state);
    }

    void Start()
    {
        //player = GetComponent<PlayerActivate>();
    }
}
