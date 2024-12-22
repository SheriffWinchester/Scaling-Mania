using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public List<GameObject> chunks = new List<GameObject>();
    public GameObject prefabLevelChunk;
    GameObject startChunk;
    GameObject player;
    bool startPlatformsSpawn = false;
    GameObject chunk;
    void Start()
    { 
        player = GameObject.Find("Player");

        //Spawn first three chunks, append to a list.
        var startChunk = Instantiate(prefabLevelChunk, new Vector3(0, 5.2f, 0), Quaternion.identity);//Center of the game shifted a bit, that the player could be on the 0 coordinates
        var secondChunk = Instantiate(prefabLevelChunk, new Vector3(startChunk.transform.position.x, startChunk.transform.position.y + 10f, startChunk.transform.position.z), Quaternion.identity);
        var thirdChunk = Instantiate(prefabLevelChunk, new Vector3(secondChunk.transform.position.x, secondChunk.transform.position.y + 10f, secondChunk.transform.position.z), Quaternion.identity);
        Debug.Log(startChunk.GetComponent<BoxCollider2D>().bounds.size);

        chunks.Add(startChunk);
        chunks.Add(secondChunk);
        chunks.Add(thirdChunk);

        Singleton.instance.startPlatformsSpawn = true;
    }

    void Update()
    {
        SpawnChunks();
        Singleton.instance.spawnChunk = false;
    }

    void SpawnChunks()
    {
        Debug.Log("Difficulty score: " + Singleton.instance.difficultyScore);
        BoxCollider2D secondObject = chunks[1].GetComponent<BoxCollider2D>();
        if (secondObject != null)
        {
            Vector3 playerPosition = player.transform.position;

            if (playerPosition.y > secondObject.bounds.max.y) //Check whether the player crossed the top border of the second chunk, then spawn ontop of it and destroy the first chunk.
            {
                Singleton.instance.spawnChunk = true;
                Debug.Log("Works");

                chunk = Instantiate(prefabLevelChunk, new Vector3(chunks[2].transform.position.x, chunks[2].transform.position.y + 10f, chunks[2].transform.position.z), Quaternion.identity);
                Debug.Log("Chunks: " + chunks.Count);
                chunks.Add(chunk);
                Destroy(chunks[0]); //!!!!!!!!!!!!TODO!!!!!!!!!!!!!! Probably to disable this chunk instead of destroying it, to avoid memory leaks.
                chunks.RemoveAt(0);

                Singleton.instance.difficultyScore++; //Increase difficulty score of the game. After 3 it begins to spawn other platforms and enemies.
                
            }
        }
        else
        {
            Debug.LogWarning("The second element does not have a BoxCollider.");
        }
    }
}
