using UnityEngine;
using UnityEngine.UI;

public class GrappleDirectionTouchUI : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform grappleDirectionUI;
    public Canvas canvas; // assign your Canvas in Inspector

    public float activationDistance = 20f;
    public float maxRadius = 100f;

    private Vector2 startPos;
    public Vector2 direction;
    private bool isDragging = false;
    public bool circleOut = false;
    private bool uiVisible = false;
    private bool hasTriggeredCircleOut = false; // Add this flag

    void Start()
    {
        if (grappleDirectionUI != null)
            grappleDirectionUI.gameObject.SetActive(false);
    }

    void Update()
    {
        // Reset circleOut at the beginning of each frame
        if (circleOut)
        {
            circleOut = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out startPos);
            Debug.Log("Start Pos: " + startPos);
            isDragging = true;
            uiVisible = false;
            hasTriggeredCircleOut = false; // Reset flag when starting new drag
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out Vector2 currentPos);
            Debug.Log("Current Pos: " + currentPos);
            float distance = Vector2.Distance(currentPos, startPos);

            if (!uiVisible && distance > activationDistance)
            {
                Debug.Log("UI Activated");
                uiVisible = true;
                grappleDirectionUI.gameObject.SetActive(true);
                grappleDirectionUI.anchoredPosition = startPos;
            }

            if (uiVisible)
            {
                direction = currentPos - startPos;
                Debug.Log("Direction: " + direction);
                Debug.Log("Direction magnitude: " + direction.magnitude);
                
                // Only trigger circleOut once per drag gesture
                if (direction.magnitude > maxRadius && !hasTriggeredCircleOut)
                {
                    uiVisible = false;
                    grappleDirectionUI.gameObject.SetActive(false);
                    circleOut = true;
                    hasTriggeredCircleOut = true; // Mark that we've triggered it
                    Debug.Log("Circle Out Triggered");
                    return;
                }

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                grappleDirectionUI.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            hasTriggeredCircleOut = false; // Reset flag when releasing button
            if (uiVisible)
            {
                uiVisible = false;
                grappleDirectionUI.gameObject.SetActive(false);
                // Don't set circleOut here since we want it only when dragging beyond maxRadius
            }
        }
    }
}