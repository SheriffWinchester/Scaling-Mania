using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace State.GameState
{
    public class MenuState : _GameState
    {
        public override void InitState(GameStateController gameStateController)
        {
            base.InitState(gameStateController);

            state = GameStateController.GameState.Menu;
            Debug.Log("State: " + state);
        }
    }
}