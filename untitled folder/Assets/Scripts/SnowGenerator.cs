using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGenerator : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject player;
    int diffScore = Singleton.instance.difficultyScore;
    private float lastSnowSpawnY = 0;
    bool distanceCheck = false;
    bool spawnAgain = true;
    private int snowCounter = 1; // Counter to keep track of the number of snow objects created
    void Start()
    {
        //CHECK SNOW SPAWNING AT THE START, CONSIDER TO REDO
        player = GameObject.Find("TrackManager").transform.Find("GamePlayer").gameObject;
        //lastSnowSpawnY = Mathf.Abs(player.transform.position.y) - 11;//Initialize the last snow spawn position - 10 that it can spawn the first time
        Debug.Log("##45 Last snow spawn position: " + lastSnowSpawnY);
        //Debug.Log("##45 Calc: " + (4 - (-5)));
    }
    // Update is called once per frame
    void Update()
    {
        float playerY = player.transform.position.y;
        Debug.Log("##45 Player position: " + playerY);
        if (playerY - lastSnowSpawnY >= 10)
        {
            Invoke("StartSnowGeneration", 1f);
            spawnAgain = false;
            lastSnowSpawnY = playerY;
            Debug.Log("##45 Snow spawned");
            //Debug.Log("##45 Last snow spawn position: " + lastSnowSpawnY);
        }
        // Check if there are any snow objects in the scene
        if (GameObject.FindGameObjectsWithTag("Snow").Length == 0)
        {
            snowCounter = 1; // Reset the snow counter if there are no snow objects
        }
    }
    IEnumerator GenerateSnow()
    {
        // Determine the number of times to spawn the snow
        int spawnCount = Random.Range(1, 4);
        Debug.Log("##45 Spawn count: " + spawnCount);
        for (int i = 0; i < spawnCount; i++)
        {
            // Generate a random X position inside the viewport
            float randomX = Random.Range(0.1f, 0.9f);

            // Set the Y position to be outside of the viewport
            float yPosition = 2f; // Adjust this value as needed

            Vector3 randomViewportPosition = new Vector3(randomX, yPosition, Camera.main.nearClipPlane);
            Vector3 randomWorldPosition = Camera.main.ViewportToWorldPoint(randomViewportPosition);

            // Spawn the snow at the random position
            GameObject snow = Instantiate(snowPrefab, randomWorldPosition, Quaternion.identity);
            snow.name = "Snow" + snowCounter; // Assign a unique name to the snow object
            snowCounter++; // Increment the counter
            int delay = Random.Range(5, 11);
            Debug.Log("##45 Delay: " + delay);
            
            // Wait for a random delay before spawning the next snow
            yield return new WaitForSeconds(delay);
        }
        spawnAgain = true;
    }
    void StartSnowGeneration()
    {
        StartCoroutine(GenerateSnow());
    }
}