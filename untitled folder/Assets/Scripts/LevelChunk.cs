using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Exited");
        Singleton.instance.spawnChunk = true;
    }
}
