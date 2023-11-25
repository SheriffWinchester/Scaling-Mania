using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = this.gameObject.transform.GetChild(0);
        Debug.Log(player.name);
    }
    void Update()
    {
        if (player.position.y > 12)
        {
            this.transform.position = new Vector2(-7f, -30f);
            Debug.Log(transform.position);
        }
    }
    
}
