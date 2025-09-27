using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the parent class to all states
public class _GameState : MonoBehaviour
{
    public GameStateController.GameState state { get; protected set; }
    protected GameStateController gameStateController;
    protected MenuController menuController;

    public virtual void InitState(GameStateController gameStateController)
    {
        this.gameStateController = gameStateController;
    }
    public virtual void InitState(MenuController menuController)
    {
        this.menuController = menuController;
    }

    //Jump back to the menu before it when we press a back button or escape key
    //You have to manually hook up each back-button to this method
    public void JumpBack()
    {
        gameStateController.JumpBack();
    }
}
