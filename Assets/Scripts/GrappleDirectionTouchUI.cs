using UnityEngine;
using UnityEngine.UI;

public class GrappleDirectionMouseUI : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform grappleDirectionUI;  // Your UI object (e.g. arrow or circle)
    public float activationDistance = 20f;    // Minimum drag distance to show UI
    public float maxRadius = 100f;            // Max distance before UI disappears

    private Vector2 startPos;
    private bool isDragging = false;
    private bool uiVisible = false;

    void Start()
    {
        if (grappleDirectionUI != null)
            grappleDirectionUI.gameObject.SetActive(false);
    }

    void Update()
    {
        // Mouse button pressed
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Start Pos: " + startPos);
            isDragging = true;
            uiVisible = false;
        }

        // Mouse button held
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(currentPos, startPos);
            Debug.Log("Mouse Dragging: " + distance);
            // Show UI once we drag past activation distance
            if (!uiVisible && distance > activationDistance)
            {
                uiVisible = true;
                grappleDirectionUI.gameObject.SetActive(true);
                grappleDirectionUI.position = startPos;
            }

            if (uiVisible)
            {
                Vector2 direction = currentPos - startPos;

                // If beyond max radius → hide UI
                if (direction.magnitude > maxRadius)
                {
                    uiVisible = false;
                    grappleDirectionUI.gameObject.SetActive(false);
                    return;
                }

                // Rotate UI to face drag direction
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //grappleDirectionUI.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

        // Mouse button released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            if (uiVisible)
            {
                uiVisible = false;
                grappleDirectionUI.gameObject.SetActive(false);
            }
        }
    }
}
