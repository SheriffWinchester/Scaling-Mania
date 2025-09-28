using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace State.Menu
{
    public class PauseMenu : _State
    {
        //Specific for this state
        public override void InitMenuState(StateController stateController)
        {
            base.InitMenuState(stateController);

            state = StateController.MenuState.PauseMenu;
            Debug.Log("State: " + state);
            AdjustUI();
        }

        void Start()
        {
            
        }

        // public void JumpToSettings()
        // {
        //     stateController.SetActiveMenuState(StateController.MenuState.Settings);
        // }

        // public void JumpToHelp()
        // {
        //     stateController.SetActiveMenuState(StateController.MenuState.Help);
        // }

        public void QuitGame()
        {
            stateController.QuitGame();
        }
    }
}