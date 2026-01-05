using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platforms : MonoBehaviour
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }

    // This method will be implemented by each subclass
    //public abstract GameObject Spawn();
}
