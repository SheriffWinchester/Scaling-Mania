using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//The state machine, which keeps track of everything
public class MenuController : MonoBehaviour
{
    //Drags = the different menus we have
    public _MenuState[] allMenus;

    //The states we can choose from
    public enum MenuState
    {
        Game, Main, Settings, Help, Pause
    }

    //State-object dictionary to make it easier to activate a menu 
    private Dictionary<MenuState, _MenuState> menuDictionary = new Dictionary<MenuState, _MenuState>();

    //The current active menu
    private _MenuState activeState;

    //To easier jump back one step, we can use a stack
    //This was also suggested in the Game Programming Patterns book
    //If so we don't have to hard-code in each state what happens when we jump back one step
    private Stack<MenuState> stateHistory = new Stack<MenuState>();

    CanvasGroup canvasGroup;
    Tween transitionTween = null;
    const float fade = 1.5f;

    private bool isTransitioning = false; // Flag to track if a transition is in progress

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
        SetActiveState(MenuState.Main);
    }



    void Update()
    {
        // Prevent pressing escape during transitions
        if (isTransitioning) return;

        //Jump back one menu step when we press escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If we're already in Pause, go back to Game
            if (activeState.state == MenuState.Pause)
            {
                SetActiveState(MenuState.Game);
            }
            // If we're in Game, go to Pause
            else if (activeState.state == MenuState.Game)
            {
                SetActiveState(MenuState.Pause);
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
            SetActiveState(MenuState.Main);
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
        // Prevent new transitions while one is in progress
        if (isTransitioning) return;
        isTransitioning = true;

        // --- remember where we’re coming from ----------------------------
        _MenuState previous = activeState;

        //First check if this menu exists
        if (!menuDictionary.ContainsKey(newState))
        {
            Debug.LogWarning($"The key <b>{newState}</b> doesn't exist so you can't activate the menu!");

            return;
        }

        //Deactivate the old state
        if (previous != null)
        {
            // look on the root; if not found, look in children
            CanvasGroup prevCG =
                previous.GetComponent<CanvasGroup>() ??
                previous.GetComponentInChildren<CanvasGroup>();

            if (prevCG != null)
            {
                transitionTween.Kill(); // kill previous tween

                transitionTween = prevCG.DOFade(0f, fade)
                    .SetUpdate(true)                       // unscaled time
                    .OnComplete(() => previous.gameObject.SetActive(false));
            }
            else
            {
                previous.gameObject.SetActive(false);        // fallback
            }
        }

        activeState = menuDictionary[newState];
        activeState.gameObject.SetActive(true);

        CanvasGroup newCG = activeState.GetComponent<CanvasGroup>();
        if (newCG != null)
        {
            transitionTween.Kill(); // kill previous tween

            newCG.alpha          = 0f;
            newCG.interactable   = true;
            newCG.blocksRaycasts = true;

            transitionTween = newCG.DOFade(1f, fade).SetUpdate(true);
        }

        Debug.Log($"<b>{activeState.state}</b> menu activated!");
        //If we are jumping back we shouldn't add to history because then we will get doubles
        if (!isJumpingBack)
        {
            stateHistory.Push(newState);
        }

        //Pause the game, no animations or anything else should happen when the game is paused
        

        DOTween.Sequence()
           .AppendInterval(fade)          // wait for the fades (uses unscaled)
           .AppendCallback(() =>
           {
               Time.timeScale = (newState == MenuState.Pause) ? 0f : 1f;
               isTransitioning = false; // Allow new transitions after completion
           });
    }



    //Quit game
    public void QuitGame()
    {
        Debug.Log("You quit game!");
    
        Application.Quit();
    }
}