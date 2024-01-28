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
        platformType3Count, platformType4Count, platformType5Count;
    int[] platformCounts; //0 - default, 1 - slide, 2 - fall1, 3 - fall2, 4 - snow
    public GameObject player;
    int diffScore = Singleton.instance.difficultyScore;
    public GameObject trackManager;

    private const int MaxPlacementAttempts = 10; // Maximum number of attempts to place a platform
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trackManager = GameObject.Find("TrackManager");
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
        numberOfPlatforms = Random.Range(20, 35);
    }
    int[] Fraction(int numberOfPlatforms, float[] fractions)
    {
        platformCounts = new int[fractions.Length];

        for (int i = 0; i < fractions.Length; i++)
        {
            platformCounts[i] = (int)(numberOfPlatforms * fractions[i]);
        }

        return platformCounts;
    }
    
    void GeneratePlatforms()
    {

        Debug.Log("Number of platforms: " + numberOfPlatforms);
        GameObject platform = null;
        Vector2 spawnPosition = GetRandomPosition();

        if (diffScore < 1)
        {
            float[] fractions = new float[] { 0.5f, 0.3f, 0.2f, 0.0f, 0.0f }; // Fractions for each platform type
            platformCounts = Fraction(numberOfPlatforms, fractions);
            // platformType1Count = Fraction(numberOfPlatforms, 0.6f);
            // platformType2Count = 0;
            // platformType3Count = 0;
            // platformType4Count = numberOfPlatforms - platformType1Count;
            // platformType5Count = 
        }
        else if (diffScore >= 1 && diffScore < 3)
        {
            float[] fractions = new float[] { 0.2f, 0.4f, 0.2f, 0.1f, 0.0f }; // Fractions for each platform type
            platformCounts = Fraction(numberOfPlatforms - Random.Range(3, 5), fractions);//Number of platforms reduced
        }
        else if (diffScore >= 3)
        {
            float[] fractions = new float[] { 0.1f, 0.3f, 0.3f, 0.3f, 0.0f }; // Fractions for each platform type
            platformCounts = Fraction(numberOfPlatforms - Random.Range(3, 8), fractions);//Number of platforms reduced
        }


        for (int i0 = 0; i0 < platformCounts[0]; i0++)//Spawn default platforms
        {
            platform = PlacePlatform(GetRandomPosition(), 0); 
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + i0;
                platform.transform.SetParent(trackManager.transform);
            }
        }
        for (int i1 = 0; i1 < platformCounts[1]; i1++)//Spawn slide platforms
        {
            platform = PlacePlatform(GetRandomPosition(), 1); 
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + i1;
                platform.transform.SetParent(trackManager.transform);
            }
        }
        for (int i2 = 0; i2 < platformCounts[2]; i2++)//Spawn fall 1 type platforms
        {
            platform = PlacePlatform(GetRandomPosition(), 2); 
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + i2;
                platform.transform.SetParent(trackManager.transform);
            }
        }
        for (int i3 = 0; i3 < platformCounts[3]; i3++)//Spawn fall 2 type platforms
        {
            platform = PlacePlatform(GetRandomPosition(), 3); 
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + i3;
                platform.transform.SetParent(trackManager.transform);
            }
        }
        for (int i4 = 0; i4 < platformCounts[4]; i4++)//Spawn snow 
        {
            platform = PlacePlatform(GetRandomPosition(), 4); 
            if (platform != null) // If a platform was successfully placed
            {
                platforms.Add(platform);
                platform.name = $"{platform.name} " + i4;
                platform.transform.SetParent(trackManager.transform);
            }
        }
        
        
    }
    
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //Spawn platforms always maximum on the distance of the grappling hook
    GameObject PlacePlatform(Vector2 initialPosition, int i)
    {
        int attempts = 0;
        while (attempts < MaxPlacementAttempts)
        {
            RaycastHit2D hitXleft = Physics2D.BoxCast(initialPosition, new Vector2(platformPrefab[i].transform.localScale.x, platformPrefab[i].transform.localScale.y + 0.01f), 0, Vector2.left);
            RaycastHit2D hitXright = Physics2D.BoxCast(initialPosition, new Vector2(platformPrefab[i].transform.localScale.x, platformPrefab[i].transform.localScale.y + 0.01f), 0, Vector2.right);
            // If the position is not too close to other platforms and no platforms on the left and right, then place the platform
            if (hitXleft.collider == null && hitXright.collider == null && !IsPositionCloseToOthers(initialPosition))
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