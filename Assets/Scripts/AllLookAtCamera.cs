using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLookAtCamera : MonoBehaviour
{
    private Transform[] childs;
    private Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam =Camera.main;
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
            if (childs[i].GetComponent<SpriteRenderer>())
            {
                childs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < childs.Length; i++)
        {

           // childs[i].transform.LookAt(Cam.transform);
            childs[i].rotation = Camera.main.transform.rotation;

        }
    }
}
