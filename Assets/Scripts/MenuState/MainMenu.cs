using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace State.Menu
{
    public class MainMenu : _MenuState
    {
        public GameObject trackManager;
        public Camera mainCamera; // Reference to the main camera
        public float moveDuration = 2f; // Duration of the camera move

        private GameObject menuPlayer;
        private GrapplingGun grapplingGun;
        private Vector2 grapplePoint;

        //Specific for this state
        public override void InitState(MenuController menuController)
        {
            base.InitState(menuController);

            state = MenuController.MenuState.Main;
        }

        public void PlayButton()
        {
            menuPlayer = GameObject.Find("MenuPlayer");
            grapplingGun = menuPlayer.GetComponent<GrapplingGun>();
            
            menuPlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            menuPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Singleton.instance.playerObjectMenuReady = true;
            grapplingGun.SetGrapplePoint();

            trackManager.SetActive(true);
            Debug.Log("Play Button Pressed");

            //Move the camera, change the state to Game and set the canvas to active
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
            });
            //NPC on the main menu is not active after the camera move
            menuPlayer.SetActive(false);
        }   
        public void JumpToGame()
        {
            menuController.SetActiveState(MenuController.MenuState.Game);
        }

        public void QuitGame()
        {
            menuController.QuitGame();
        }
    }
}