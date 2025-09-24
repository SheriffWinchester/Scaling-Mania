using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace State.Menu
{
    public class PauseMenu : _MenuState
    {
        //Specific for this state
        public override void InitState(MenuController menuController)
        {
            base.InitState(menuController);

            state = MenuController.MenuState.PauseMenu;
            Debug.Log("State: " + state);
            AdjustUI();
        }

        void Start()
        {
            
        }

        // public void JumpToSettings()
        // {
        //     menuController.SetActiveState(MenuController.MenuState.Settings);
        // }

        // public void JumpToHelp()
        // {
        //     menuController.SetActiveState(MenuController.MenuState.Help);
        // }

        public void QuitGame()
        {
            menuController.QuitGame();
        }
    }
}