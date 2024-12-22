using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance; //{ get; private set; }
    public bool spawnChunk = false;
    public bool startPlatformsSpawn = false;
    public int difficultyScore = 0;
    public bool snowDisabledCollision = false;
    public int mainScore = 0;
    public bool menuCamera = false;

    public bool gameStarted = false;
    public bool gameStartedMoveCamera = false;
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
}