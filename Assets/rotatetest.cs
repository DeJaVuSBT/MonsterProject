using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatetest : MonoBehaviour
{
    [Range(0,1)]
    public float speed = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(Vector3.up, speed);
    }
}
