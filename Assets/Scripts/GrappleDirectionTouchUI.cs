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

    void Start()
    {
        if (grappleDirectionUI != null)
            grappleDirectionUI.gameObject.SetActive(false);
    }

    void Update()
    {
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
                grappleDirectionUI.anchoredPosition = startPos; // ✅ use anchoredPosition, not position
            }

            if (uiVisible)
            {
                direction = currentPos - startPos;
                Debug.Log("Direction: " + direction);
                Debug.Log("Direction magnitude: " + direction.magnitude);
                if (direction.magnitude > maxRadius)
                {
                    uiVisible = false;
                    grappleDirectionUI.gameObject.SetActive(false);
                    circleOut = true;
                    return;
                }

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                grappleDirectionUI.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            circleOut = false;
            if (uiVisible)
            {
                uiVisible = false;
                grappleDirectionUI.gameObject.SetActive(false);
                circleOut = true;
            }
        }
    }
}
