using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrack : MonoBehaviour
{
    float previousYPosition;

    void Start()
    {
        // Store the initial Y position of the player
        previousYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved by 1 unit in the Y axis
        if (transform.position.y - previousYPosition >= 1)
        {
            // Increase the score
            Singleton.instance.mainScore += 1;

            // Update the previous Y position
            previousYPosition = transform.position.y;
            Debug.Log("Score: " + Singleton.instance.mainScore);
        }
    }
}
