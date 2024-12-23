using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuButton : MonoBehaviour
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
        //grapplingGun.SetGrapplePoint();

        trackManager.SetActive(true);
        //Singleton.instance.gameStartedMoveCamera = true;
        Debug.Log("Play Button Pressed");

        mainCamera.transform.DOMove(new Vector3(0, 4.6f, -10f), 4).OnComplete(() =>
        {
            Debug.Log("Camera moved to target position");
            Singleton.instance.gameStarted = true;
            canvas.SetActive(true);
            canvas2.SetActive(false);
            Debug.Log("gameStartedMoveCamera set to true");
        });
        
        //StartCoroutine(MoveCamera(new Vector3(0, 4.6f, -10f), moveDuration));
    }

    private IEnumerator MoveCamera(Vector3 targetPosition, float duration)
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