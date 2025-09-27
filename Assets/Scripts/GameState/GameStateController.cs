using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The state machine, which keeps track of everything
public class GameStateController : MonoBehaviour
{
    //Drags = the different states we have
    public _GameState[] allGameStates;

    //The states we can choose from
    public enum GameState
    {
        Game, Menu, GameOver
    }

    //State-object dictionary to make it easier to activate a state
    private Dictionary<GameState, _GameState> gameDictionary = new Dictionary<GameState, _GameState>();

    //The current active state
    private _GameState activeState;

    //To easier jump back one step, we can use a stack
    //This was also suggested in the Game Programming Patterns book
    //If so we don't have to hard-code in each state what happens when we jump back one step
    private Stack<GameState> stateHistory = new Stack<GameState>();



    void Start()
    {
        //Put all states into a dictionary
        foreach (_GameState gameState in allGameStates)
        {
            if (gameState == null)
            {
                continue;
            }

            //Inject a reference to this script into all states
            gameState.InitState(gameStateController: this);

            //Check if this key already exists, because it means we have forgotten to give a state its unique key
            if (gameDictionary.ContainsKey(gameState.state))
            {
                Debug.LogWarning($"The key <b>{gameState.state}</b> already exists in the game dictionary!");

                continue;
            }

            gameDictionary.Add(gameState.state, gameState);
        }

        //Deactivate all states
        foreach (GameState state in gameDictionary.Keys)
        {
            gameDictionary[state].gameObject.SetActive(false);
        }

        //Activate the default state
        SetActiveState(GameState.Game);
    }



    void Update()
    {

    }



    //Jump back one step = what happens when we press escape or one of the back buttons
    public void JumpBack()
    {
        //If we have just one item in the stack then, it means we are at the state we set at start, so we have to jump forward
        if (stateHistory.Count <= 1)
        {
            SetActiveState(GameState.Game);
        }
        else
        {
            //Remove one from the stack
            stateHistory.Pop();

            //Activate the menu that's on the top of the stack
            SetActiveState(stateHistory.Peek(), isJumpingBack: true);
        }
    }


    //Activate a game state
    public void SetActiveState(GameState newState, bool isJumpingBack = false)
    {
        if (!gameDictionary.ContainsKey(newState))
        {
            Debug.LogWarning($"The key <b>{newState}</b> doesn't exist so you can't activate the game state!");
            return;
        }

        activeState = gameDictionary[newState];

        Debug.Log($"<b>{activeState.state}</b> game state activated!");

        if (!isJumpingBack)
        {
            stateHistory.Push(newState);
        }

        if (newState == GameState.Menu)
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
