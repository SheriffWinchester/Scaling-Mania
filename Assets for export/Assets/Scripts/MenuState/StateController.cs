using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The state machine, which keeps track of everything
public class StateController : MonoBehaviour
{
    //Drags = the different menus we have
    public _State[] allMenus;
    public _State[] globalStates;

    //The states we can choose from
    public enum MenuState
    {
        Game, MainMenu, SettingsMenu, HelpMenu, PauseMenu, GameOver
    }
    public enum GlobalState
    {
        Title, Game, Menu, GameOver
    }

    //State-object dictionary to make it easier to activate a menu 
    private Dictionary<MenuState, _State> menuDictionary = new Dictionary<MenuState, _State>();
    private Dictionary<GlobalState, _State> globalStateDictionary = new Dictionary<GlobalState, _State>();

    //The current active menu
    public _State activeMenuState;
    public _State activeGlobalState;

    //To easier jump back one step, we can use a stack
    //This was also suggested in the Game Programming Patterns book
    //If so we don't have to hard-code in each state what happens when we jump back one step
    private Stack<MenuState> menuStateHistory = new Stack<MenuState>();
    private Stack<GlobalState> globalStateHistory = new Stack<GlobalState>();



    void Start()
    {
        //Put all menus into a dictionary
        foreach (_State menu in allMenus)
        {
            if (menu == null)
            {
                continue;
            }

            //Inject a reference to this script into all menus
            menu.InitMenuState(stateController: this);

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
        SetActiveMenuState(MenuState.MainMenu);

        //Put all states into a dictionary
        foreach (_State gState in globalStates)
        {
            if (gState == null)
            {
                continue;
            }

            //Inject a reference to this script into all menus
            gState.InitGlobalState(stateController: this);

            //Check if this key already exists, because it means we have forgotten to give a menu its unique key
            if (globalStateDictionary.ContainsKey(gState.globalState))
            {
                Debug.LogWarning($"The key <b>{gState.globalState}</b> already exists in the menu dictionary!");

                continue;
            }

            globalStateDictionary.Add(gState.globalState, gState);
        }

        //Deactivate all menus
        foreach (GlobalState gState in globalStateDictionary.Keys)
        {
            globalStateDictionary[gState].gameObject.SetActive(false);
        }

        //Activate the default menu
        SetActiveGlobalState(GlobalState.Title);
     
    }



    void Update()
    {
        Debug.Log("Active Global State: " + activeGlobalState.globalState);
        //DebugMenuStateHistory();
        //Jump back one menu step when we press escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //activeState.JumpBack();
            // If we're already in Pause, go back to Game
            if (activeMenuState.state == MenuState.PauseMenu)
            {
                SetActiveMenuState(MenuState.Game);
            }
            // If we're in Game, go to Pause
            else if (activeMenuState.state == MenuState.Game)
            {
                SetActiveMenuState(MenuState.PauseMenu);
            }
            else
            {
                // For other menus, jump back
                activeMenuState.JumpBack();
            }
        }
    }



    //Jump back one step = what happens when we press escape or one of the back buttons
    public void JumpBack()
    {
        //If we have just one item in the stack then, it means we are at the state we set at start, so we have to jump forward
        if (menuStateHistory.Count <= 1)
        {
            SetActiveMenuState(MenuState.MainMenu);
        }
        else
        {
            //Remove one from the stack
            menuStateHistory.Pop();

            //Activate the menu that's on the top of the stack
            SetActiveMenuState(menuStateHistory.Peek(), isJumpingBack: true);
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
    public void SetActiveMenuState(MenuState newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState))
        {
            Debug.LogWarning($"The key <b>{newState}</b> doesn't exist so you can't activate the menu!");
            return;
        }

        if (activeMenuState != null)
        {
            activeMenuState.Hide(); // smooth fade out
        }

        activeMenuState = menuDictionary[newState];
        activeMenuState.Show(); // smooth fade in

        Debug.Log($"<b>{activeMenuState.state}</b> menu activated!");

        if (!isJumpingBack)
        {
            menuStateHistory.Push(newState);
        }
        if (activeMenuState.state == MenuState.Game)
        {
            SetActiveGlobalState(GlobalState.Game);
        }
    }
    
    public void SetActiveGlobalState(GlobalState newState, bool isJumpingBack = false)
    {
        if (!globalStateDictionary.ContainsKey(newState))
        {
            Debug.LogWarning($"The key <b>{newState}</b> doesn't exist so you can't activate the menu!");
            return;
        }

        activeGlobalState = globalStateDictionary[newState];

        Debug.Log($"<b>{activeGlobalState.state}</b> menu activated!");

        if (!isJumpingBack)
        {
            globalStateHistory.Push(newState);
        }
        if (activeGlobalState.globalState == GlobalState.Menu)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void DebugMenuStateHistory()
    {
        Debug.Log($"MenuStateHistory Count: {menuStateHistory.Count}");
        
        if (menuStateHistory.Count == 0)
        {
            Debug.Log("MenuStateHistory is empty");
            return;
        }

        // Convert stack to array to see all items (without modifying the stack)
        MenuState[] historyArray = menuStateHistory.ToArray();
        
        Debug.Log("MenuStateHistory contents (top to bottom):");
        for (int i = 0; i < historyArray.Length; i++)
        {
            Debug.Log($"StatesHistory  [{i}]: {historyArray[i]}");
        }
        
        if (menuStateHistory.Count > 0)
        {
            Debug.Log($"Current top of stack (Peek): {menuStateHistory.Peek()}");
        }
    }


    //Quit game
    public void QuitGame()
    {
        Debug.Log("You quit game!");

        Application.Quit();
    }
}
