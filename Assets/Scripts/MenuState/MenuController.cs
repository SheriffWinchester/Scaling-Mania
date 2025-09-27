using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The state machine, which keeps track of everything
public class MenuController : MonoBehaviour
{
    //Drags = the different menus we have
    public _MenuState[] allMenus;

    //The states we can choose from
    public enum MenuState
    {
        Game, MainMenu, SettingsMenu, HelpMenu, PauseMenu, GameOver
    }

    //State-object dictionary to make it easier to activate a menu 
    private Dictionary<MenuState, _MenuState> menuDictionary = new Dictionary<MenuState, _MenuState>();

    //The current active menu
    private _MenuState activeState;

    //To easier jump back one step, we can use a stack
    //This was also suggested in the Game Programming Patterns book
    //If so we don't have to hard-code in each state what happens when we jump back one step
    private Stack<MenuState> stateHistory = new Stack<MenuState>();



    void Start()
    {
        //Put all menus into a dictionary
        foreach (_MenuState menu in allMenus)
        {
            if (menu == null)
            {
                continue;
            }

            //Inject a reference to this script into all menus
            menu.InitState(menuController: this);

            //Check if this key already exists, because it means we have forgotten to give a menu its unique key
            if (menuDictionary.ContainsKey(menu.state))
            {
                Debug.LogWarning($"The key <b>{menu.state}</b> already exists in the menu dictionary!");

                continue;
            }
        
            menuDictionary.Add(menu.state, menu);
        }

        //Deactivate all menus
        foreach (MenuState state in menuDictionary.Keys)
        {
            menuDictionary[state].gameObject.SetActive(false);
        }

        //Activate the default menu
        SetActiveState(MenuState.MainMenu);
    }



    void Update()
    {
        //Jump back one menu step when we press escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //activeState.JumpBack();
             // If we're already in Pause, go back to Game
            if (activeState.state == MenuState.PauseMenu)
            {
                SetActiveState(MenuState.Game);
            }
            // If we're in Game, go to Pause
            else if (activeState.state == MenuState.Game)
            {
                SetActiveState(MenuState.PauseMenu);
            }
            else
            {
                // For other menus, jump back
                activeState.JumpBack();
            }
        }
    }



    //Jump back one step = what happens when we press escape or one of the back buttons
    public void JumpBack()
    {
        //If we have just one item in the stack then, it means we are at the state we set at start, so we have to jump forward
        if (stateHistory.Count <= 1)
        {
            SetActiveState(MenuState.MainMenu);
        }
        else
        {
            //Remove one from the stack
            stateHistory.Pop();

            //Activate the menu that's on the top of the stack
            SetActiveState(stateHistory.Peek(), isJumpingBack: true);
        }
    }

    public void AdjustUI()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;

        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
    }



    //Activate a menu
    public void SetActiveState(MenuState newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState))
        {
            Debug.LogWarning($"The key <b>{newState}</b> doesn't exist so you can't activate the menu!");
            return;
        }

        if (activeState != null)
        {
            activeState.Hide(); // smooth fade out
        }

        activeState = menuDictionary[newState];
        activeState.Show(); // smooth fade in

        Debug.Log($"<b>{activeState.state}</b> menu activated!");

        if (!isJumpingBack)
        {
            stateHistory.Push(newState);
        }

        if (newState == MenuState.PauseMenu || newState == MenuState.SettingsMenu || newState == MenuState.HelpMenu)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }




    //Quit game
    public void QuitGame()
    {
        Debug.Log("You quit game!");
    
        Application.Quit();
    }
}
