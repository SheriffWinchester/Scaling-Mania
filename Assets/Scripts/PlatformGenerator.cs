using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] platformPrefab;
    public int numberOfPlatforms;
    // public int PlatNum1;
    // public int PlatNum2;
    public float minY, maxY, minX, maxX;
    public int fractionOfPlatforms;
    public float minDistanceBetweenPlatforms = 3f;
    private List<GameObject> platforms = new List<GameObject>();
     int platformType1Count, platformType2Count, 
        platformType3Count, platformType4Count;

    private const int MaxPlacementAttempts = 10; // Maximum number of attempts to place a platform
    void Start()
    {
        InitializeBounds();
        Debug.Log("Fraction of platforms: " + fractionOfPlatforms);
        GeneratePlatforms();
    }

    void InitializeBounds()
    {
        minY = transform.localPosition.y - 4.5f;
        maxY = transform.localPosition.y + 4.5f;
        minX = transform.localPosition.x - 2f;
        maxX = transform.localPosition.x + 2f;
        numberOfPlatforms = Random.Range(15, 30);
    }
    int Fraction(int numberOfPlatforms, float fraction)
    {
        return (int)(numberOfPlatforms * fraction);
    }
    

    void GeneratePlatforms()
    {
        int diffScore = Singleton.instance.difficultyScore;

        Debug.Log("Number of platforms: " + numberOfPlatforms);
        GameObject platform = null;
        Vector2 spawnPosition = GetRandomPosition();
        if (diffScore < 1)
        {
            platformType1Count = Fraction(numberOfPlatforms, 0.6f);
            platformType2Count = 0;
            platformType4Count = numberOfPlatforms - platformType1Count;
        }
        else if (diffScore >= 1 && diffScore <= 3)
        {
            platformType1Count = Fraction(numberOfPlatforms, 0.6f);
            platformType2Count = numberOfPlatforms - platformType1Count;
        }
        //Spawn default platforms
        for (int j = 0; j < platformType1Count; j++)
        {
            platform = PlacePlatform(GetRandomPosition(), 0); // Assuming 0 is the index for the first type of platform
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + j;
            }
        }
        //Spawn fall 1 platforms
        for (int k = 0; k < platformType2Count; k++)
        {
            platform = PlacePlatform(GetRandomPosition(), 1); // Assuming 1 is the index for the second type of platform
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + k;
            }
        }
        //Spawn slide platforms
        for (int l = 0; l < platformType4Count; l++)
        {
            platform = PlacePlatform(GetRandomPosition(), 3); // Assuming 1 is the index for the second type of platform
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + l;
            }
        }
        
        
    }
    

    GameObject PlacePlatform(Vector2 initialPosition, int i)
    {
        int attempts = 0;
        while (attempts < MaxPlacementAttempts)
        {
            if (!IsPositionCloseToOthers(initialPosition))
            {
                Debug.Log("Attempts made: " + attempts);
                return Instantiate(platformPrefab[i], initialPosition, Quaternion.identity);
            }

            // If the position is too close to other platforms, get a new random position and try again
            initialPosition = GetRandomPosition();
            attempts++;
        }

        Debug.LogWarning("Failed to place a platform after " + MaxPlacementAttempts + " attempts.");
        return null; // Return null if a suitable position wasn't found
    }

    bool IsPositionCloseToOthers(Vector2 position)
    {
        //This function allocate memory, reconsider to change later.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, minDistanceBetweenPlatforms, 1 << 8 | 1 << 6);
        return hitColliders.Length > 0; // If any colliders are found, the position is considered too close
    }

    Vector2 GetRandomPosition()
    {
        float randomY = Random.Range(minY, maxY);
        float randomX = Random.Range(minX, maxX);
        return new Vector2(randomX, randomY);
    }
}