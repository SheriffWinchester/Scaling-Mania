using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGenerator : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject player;
    int diffScore = Singleton.instance.difficultyScore;
    private float lastSnowSpawnY;
    bool distanceCheck = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastSnowSpawnY = Mathf.Abs(player.transform.position.y) - 11;//Initialize the last snow spawn position - 10 that it can spawn the first time
        //Debug.Log("##45 Last snow spawn position: " + lastSnowSpawnY);
        //Debug.Log("##45 Calc: " + (4 - (-5)));
        //StartCoroutine(GenerateSnow());
    }
    // Update is called once per frame
    void Update()
    {
        float playerY = player.transform.position.y;
        //Debug.Log("##45 Player position: " + playerY);
        if (playerY - lastSnowSpawnY >= 10)
        {
            StartCoroutine(GenerateSnow());

            lastSnowSpawnY = playerY;
            Debug.Log("##45 Snow spawned");
            //Debug.Log("##45 Last snow spawn position: " + lastSnowSpawnY);
        }
    }

    
    // void GenerateSnow()
    // {
    //     int num1 = Random.Range(0, 2);
    //     int num2 = Random.Range(0, 2);

    //     if (diffScore < 1)
    //     {
    //         Debug.Log("Player position: " + player.transform.position.y);
    //         Debug.Log("Last snow spawn position: " + lastSnowSpawnY);
    //         float playerY = Mathf.Abs(player.transform.position.y);
    //         if (playerY - lastSnowSpawnY >= 10)
    //         {
    //            // Generate a random X position inside the viewport
    //             float randomX = Random.Range(0f, 1f);

    //             // Set the Y position to be outside of the viewport
    //             float yPosition = 2f; // Adjust this value as needed

    //             Vector3 randomViewportPosition = new Vector3(randomX, yPosition, Camera.main.nearClipPlane);
    //             Vector3 randomWorldPosition = Camera.main.ViewportToWorldPoint(randomViewportPosition);

    //             // Spawn the snow at the random position
    //             Instantiate(snowPrefab, randomWorldPosition, Quaternion.identity);

    //             // Update the Y position when the last snow was spawned
    //             lastSnowSpawnY = playerY;
    //             Debug.Log("Snow spawned");
    //         }
    //     }
    // }
    IEnumerator GenerateSnow()
    {
        // Determine the number of times to spawn the snow
        int spawnCount = Random.Range(1, 4);
        Debug.Log("##45 Spawn count: " + spawnCount);
        for (int i = 0; i < spawnCount; i++)
        {
            // Generate a random X position inside the viewport
            float randomX = Random.Range(0f, 1f);

            // Set the Y position to be outside of the viewport
            float yPosition = 2f; // Adjust this value as needed

            Vector3 randomViewportPosition = new Vector3(randomX, yPosition, Camera.main.nearClipPlane);
            Vector3 randomWorldPosition = Camera.main.ViewportToWorldPoint(randomViewportPosition);

            // Spawn the snow at the random position
            Instantiate(snowPrefab, randomWorldPosition, Quaternion.identity);
            int delay = Random.Range(5, 11);
            Debug.Log("##45 Delay: " + delay);
            // Wait for a random delay before spawning the next snow
            yield return new WaitForSeconds(delay);
        }
    }
}