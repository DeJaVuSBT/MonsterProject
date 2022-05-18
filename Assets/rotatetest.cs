using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatetest : MonoBehaviour
{
   
    public float speed = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.RotateAround(transform.TransformPoint(Vector3.up), transform.TransformPoint(Vector3.up), Time.deltaTime * speed);
        //transform.RotateAround(transform.up, transform.TransformPoint(Vector3.up), Time.deltaTime * speed);
        
    }
}
