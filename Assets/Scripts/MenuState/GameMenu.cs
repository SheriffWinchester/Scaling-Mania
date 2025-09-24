using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : _MenuState
{
    //Specific for this state
    public override void InitState(MenuController menuController)
    {
        base.InitState(menuController);

        state = MenuController.MenuState.Game;
        Debug.Log("State: " + state);
    }

}
