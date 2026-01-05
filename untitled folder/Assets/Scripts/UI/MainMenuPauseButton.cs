using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainMenuPauseButton : MonoBehaviour
{
    public GameObject trackManager;
    public GameObject canvas;
    public GameObject canvas2;
    public Camera mainCamera; // Reference to the main camera
    public float moveDuration = 2f; // Duration of the camera move

    private GameObject menuPlayer;
    private GrapplingGun grapplingGun;
    private Vector2 grapplePoint;

    public void PlayButton()
    {
        menuPlayer = GameObject.Find("MenuPlayer");
        grapplingGun = menuPlayer.GetComponent<GrapplingGun>();
        
        // Set the grapple point to (0, 1)
        //Vector3 newGrapplePoint = new Vector3(0, 1, 0);
        //grapplingGun.distanceVector = newGrapplePoint - grapplingGun.gunPivot.position;
        menuPlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        menuPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Singleton.instance.playerObjectMenuReady = true;
        //grapplingGun.distanceVector = new Vector3(0, 1, 0);
        grapplingGun.SetGrapplePoint();

        trackManager.SetActive(true);
        //Singleton.instance.gameStartedMoveCamera = true;
        Debug.Log("Play Button Pressed");

        Invoke("MoveCamera", 1f);
        
        //StartCoroutine(MoveCamera1(new Vector3(0, 4.6f, -10f), moveDuration));
    }

    void MoveCamera()
    {
        mainCamera.transform.DOMove(new Vector3(0, 4.6f, -10f), 2).OnComplete(() =>
        {
            Debug.Log("Camera moved to target position");
            Singleton.instance.gameStarted = true;
            canvas.SetActive(true);
            canvas2.SetActive(false);
        });
        menuPlayer.SetActive(false);
    }   

    private IEnumerator MoveCamera1(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = mainCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // Ensure the camera reaches the target position
    }
}