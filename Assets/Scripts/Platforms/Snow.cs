using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snow : MonoBehaviour
{
    public RectTransform uiSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            float yPosition = Screen.height;
            uiSprite.position = new Vector2(transform.position.x, yPosition);
        } 
        else
        {
            uiSprite.position = Vector2.zero;
        }
    }
}
