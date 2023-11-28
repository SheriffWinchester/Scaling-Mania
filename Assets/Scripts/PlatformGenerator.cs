using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms;
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;
    public float minDistanceBetweenPlatforms = 1.5f;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        minY = transform.localPosition.y - 4.5f;
        maxY = transform.localPosition.y + 4.5f;
        minX = transform.localPosition.x - 2f;
        maxX = transform.localPosition.x + 2f;
  
        numberOfPlatforms = Random.Range(4, 8);
        Vector2 spawnPosition = new Vector2();

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y = Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(minX, maxX);

            //Check proximity to existing platforms

            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            platforms.Add(platform);

            while (IsTooCloseToExistingPlatform(spawnPosition) == true)
            {
                spawnPosition.x = Random.Range(minX, maxX);
                spawnPosition.y = Random.Range(minY, maxY);
            }
        }

        Singleton.instance.startPlatformsSpawn = false;
    }
    bool IsTooCloseToExistingPlatform(Vector2 position)
    {
        foreach (GameObject platform in platforms)
        {
            float distance = Vector2.Distance(position, platform.transform.position);
            if (distance < minDistanceBetweenPlatforms)
            {
                Debug.Log("Close");
                return true;
            }
        }
        return false;
    }
}
