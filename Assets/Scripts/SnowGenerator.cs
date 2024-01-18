using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGenerator : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject player;
    int diffScore = Singleton.instance.difficultyScore;
    void Start()
    {
        GenerateSnow();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
    void GenerateSnow()
    {
        int num1 = Random.Range(0, 2);
        int num2 = Random.Range(0, 2);

        // if (diffScore < 1)
        // {
            // Generate a random X position inside the viewport
            float randomX = Random.Range(0f, 1f);

            // Set the Y position to be outside of the viewport
            float yPosition = 1.1f; // Adjust this value as needed

            Vector3 randomViewportPosition = new Vector3(randomX, yPosition, Camera.main.nearClipPlane);
            Vector3 randomWorldPosition = Camera.main.ViewportToWorldPoint(randomViewportPosition);

            // Spawn the snow at the random position
            Instantiate(snowPrefab, randomWorldPosition, Quaternion.identity);
        // }
    }
}
