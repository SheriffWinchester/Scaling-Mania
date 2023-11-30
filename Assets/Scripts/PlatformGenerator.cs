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
    public float minDistanceBetweenPlatforms = 3f;
    private List<GameObject> platforms = new List<GameObject>();
    int closeCount = 1;

    void Start()
    {
        minY = transform.localPosition.y - 4.5f;
        maxY = transform.localPosition.y + 4.5f;
        minX = transform.localPosition.x - 2f;
        maxX = transform.localPosition.x + 2f;
  
        numberOfPlatforms = Random.Range(5, 12);
        Vector2 spawnPosition = new Vector2();
        spawnPosition.y = Random.Range(minY, maxY);
        spawnPosition.x = Random.Range(minX, maxX);

        Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        int loopCounts = 0;
        for (int i = 1; i < 7; i++)
        {
            loopCounts++;
            int countChecks = 0;
            
            spawnPosition.y = Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(minX, maxX);
            //Check proximity to existing platforms

            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            platforms.Add(platform);
            platform.name = "Platform " + i; 
            platform.layer = 10;
            Collider2D[] hitCollider = Physics2D.OverlapCircleAll(platform.transform.position, minDistanceBetweenPlatforms, 1<<7); 
            platform.layer = 7;
            //Debug.Log(hitCollider.name);
            while (hitCollider.Length >= 1) {
                countChecks++;
                Debug.Log("Close");
                Debug.Log("Center: " + platform.transform.position);
                for (int j = 0; j < hitCollider.Length; j++)
                {
                    Debug.Log(hitCollider[j].name);
                    Debug.Log("HitCollider: " + hitCollider[j].transform.position);
                }
                platform.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

                platform.layer = 10;
                hitCollider = Physics2D.OverlapCircleAll(platform.transform.position, minDistanceBetweenPlatforms, 1<<7); 
                platform.layer = 7;

                //Debug.Log(hitCollider.name);
            }
            Debug.Log("Count checks: " + countChecks);

            // if (IsTooCloseToExistingPlatform(spawnPosition) == true && platforms.Count > 1)
            // {
            //     //Debug.Log("Close");
            //     platforms[i].transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            //     // platforms[i].transform.position.y = Random.Range(minY, maxY);
            // }
        }
        Debug.Log("Loop counts: " + loopCounts);
        Singleton.instance.startPlatformsSpawn = false;
    }
    bool IsTooCloseToExistingPlatform(Vector2 position)
    {

        for (int i = 1; i < platforms.Count; i++)
        {
            float distance = Vector2.Distance(position, platforms[i].transform.position);
            closeCount++;
            if (distance < minDistanceBetweenPlatforms)
            {
                Debug.Log("Close");
                return true;
            }
        }
        return false;
    }
}










// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlatformGenerator : MonoBehaviour
// {
//     public GameObject platformPrefab;
//     public int numberOfPlatforms;
//     public float minY;
//     public float maxY;
//     public float minX;
//     public float maxX;
//     public float minDistanceBetweenPlatforms = 3f;
//     private List<GameObject> platforms = new List<GameObject>();
//     int closeCount = 1;

//     void Start()
//     {
//         minY = transform.localPosition.y - 4.5f;
//         maxY = transform.localPosition.y + 4.5f;
//         minX = transform.localPosition.x - 2f;
//         maxX = transform.localPosition.x + 2f;
  
//         numberOfPlatforms = Random.Range(4, 8);
//         Vector2 spawnPosition = new Vector2();
//         spawnPosition.y = Random.Range(minY, maxY);
//         spawnPosition.x = Random.Range(minX, maxX);

//         GameObject platform = Instantiate(platformPrefab, new Vector2(Random.Range(minX, maxX), Random.Range(-4f, -3.5f)), Quaternion.identity);
//         platforms.Add(platform);
//         // for (int i = 0; i < numberOfPlatforms; i++)
//         // {
//         //     GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
//         //     platforms.Add(platform);


//         // }











//         // Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
//         // int loopCounts = 0;
//         // for (int i = 1; i < numberOfPlatforms; i++)
//         // {
//         //     loopCounts++;
//         //     int countChecks = 0;
            
//         //     spawnPosition.y = Random.Range(minY, maxY);
//         //     spawnPosition.x = Random.Range(minX, maxX);
//         //     //Check proximity to existing platforms

//         //     GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
//         //     platforms.Add(platform);
//         //     platform.name = "Platform " + i; 
//         //     platform.layer = 10;
//         //     Collider2D hitCollider = Physics2D.OverlapCircle(platform.transform.position, minDistanceBetweenPlatforms, 1<<7); 
//         //     platform.layer = 7;
//         //     //Debug.Log(hitCollider.name);
//         //     while (hitCollider != null) {
//         //         countChecks++;
//         //         Debug.Log("Close");
//         //         platform.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
//         //         Debug.Log(platform.transform.position);
//         //         platform.layer = 10;
//         //         hitCollider = Physics2D.OverlapCircle(platform.transform.position, minDistanceBetweenPlatforms, 1<<7); 
//         //         platform.layer = 7;
//         //         // spawnPosition.y = Random.Range(minY, maxY);
//         //         // spawnPosition.x = Random.Range(minX, maxX);
//         //         Debug.Log(hitCollider.name);
//         //     }
//         //     Debug.Log("Count checks: " + countChecks);
//             // if (IsTooCloseToExistingPlatform(spawnPosition) == true && platforms.Count > 1)
//             // {
//             //     //Debug.Log("Close");
//             //     platforms[i].transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
//             //     // platforms[i].transform.position.y = Random.Range(minY, maxY);
//             // }
//     }
//         // Debug.Log(loopCounts);
//         // Singleton.instance.startPlatformsSpawn = false;
// }











//     // bool IsTooCloseToExistingPlatform(Vector2 position)
//     // {

//     //     for (int i = 1; i < platforms.Count; i++)
//     //     {
//     //         float distance = Vector2.Distance(position, platforms[i].transform.position);
//     //         closeCount++;
//     //         if (distance < minDistanceBetweenPlatforms)
//     //         {
//     //             Debug.Log("Close");
//     //             return true;
//     //         }
//     //     }
//     //     return false;
//     // }

