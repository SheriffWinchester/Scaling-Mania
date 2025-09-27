using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public MenuController menuController;
    public void PressButton()
    {
        menuController.SetActiveState(MenuController.MenuState.SettingsMenu);
    }
}