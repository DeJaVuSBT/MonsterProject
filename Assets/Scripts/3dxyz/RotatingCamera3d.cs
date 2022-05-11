using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera3d : MonoBehaviour
{
    public float rotateTime = 0.2f;
    private bool closeCamera = false;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 normalOffSet= Vector2.zero;
    [SerializeField]
    private Vector3 closeUpOnSet= Vector2.zero;
    void Start()
    {
        if (player==null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            
        }
       // CinematicBars.Show_Static(400, 0.3f);
    }

    void Update()
    {
        if (closeCamera)
        {
            transform.position = player.position + closeUpOnSet;
        }
        else
        {
            transform.position = player.position + normalOffSet;
        }
    }

    IEnumerator CloseUpCamera()
    {
        //moving
        if (closeCamera==false)
        {
            float dis = Vector3.Distance(transform.position, closeUpOnSet);
            if (dis > 0.1f) { 
                
            }
        }
            yield return new WaitForFixedUpdate();
        closeCamera = true;
    }
}
