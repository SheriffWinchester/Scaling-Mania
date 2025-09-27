using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResumeButton : MonoBehaviour
{
    public MenuController menuController;
    public GameStateController gameStateController;
    public void PressButton()
    {
        gameStateController.SetActiveState(GameStateController.GameState.Game);
    }
}