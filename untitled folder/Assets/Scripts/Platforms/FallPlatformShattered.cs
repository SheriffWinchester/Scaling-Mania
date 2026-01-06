using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatformShattered : MonoBehaviour
{
    void Start()
    {
        // Destroy this object after 5 seconds
        Destroy(gameObject, 5f);
    }
}
