using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ResumeButton : MonoBehaviour
{
    public MenuController menuController;
    public void PressButton()
    {
        menuController.SetActiveState(MenuController.MenuState.Game);
    }
}