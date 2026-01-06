using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(transform.position, 1.5f, 1<<7);
        Debug.Log(hitCollider.Length);
        Debug.Log(hitCollider[0].gameObject.layer);
    }
}
