using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace State.Menu
{
    public class MainMenu : _State
    {
        public GameObject trackManager;
        public Camera mainCamera; // Reference to the main camera
        public float moveDuration = 2f; // Duration of the camera move

        public GameObject menuPlayer;
        public GameObject gamePlayer;
        private GrapplingGun grapplingGun;
        private Vector2 grapplePoint;

        //Specific for this state
        public override void InitMenuState(StateController stateController)
        {
            base.InitMenuState(stateController);

            state = StateController.MenuState.MainMenu;
            Debug.Log("State: " + state);
        }

        public void PlayButton()
        {
            menuPlayer = GameObject.Find("MenuPlayer");
            gamePlayer = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject;
            grapplingGun = menuPlayer.GetComponent<GrapplingGun>();

            menuPlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            menuPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Singleton.instance.playerObjectMenuReady = true;

            try
            {
                grapplingGun.SetGrapplePoint(); // or SetGrapplePoint(true) if you use that overload
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"SetGrapplePoint threw: {ex}");
            }

            Debug.Log("Play Button Pressed");
            trackManager.SetActive(true);

            stateController.SetActiveGlobalState(StateController.GlobalState.Game);
            Invoke("MoveCamera", 1f);
        }

        void MoveCamera()
        {
            mainCamera.transform.DOMove(new Vector3(0, 4.6f, -10f), 2).OnComplete(() =>
            {
                Debug.Log("Camera moved to target position");
                Singleton.instance.gameStarted = true;

                //Change the state to Game and set the canvas to active
                JumpToGame();
                gamePlayer.GetComponent<PlayerActivate>().ActivatePlayer(); //Activate the player scripts
            });
            //NPC on the main menu is not active after the camera move
            menuPlayer.SetActive(false);
            gamePlayer.SetActive(true);
        }   
        public void JumpToGame()
        {
            stateController.SetActiveMenuState(StateController.MenuState.Game);
        }

        public void QuitGame()
        {
            stateController.QuitGame();
        }
    }
}