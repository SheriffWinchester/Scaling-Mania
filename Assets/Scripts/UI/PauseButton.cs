using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PauseButton : MonoBehaviour
{
    public MenuController menuController;
    public void PressButton()
    {
        menuController.SetActiveState(MenuController.MenuState.PauseMenu);
    }
}