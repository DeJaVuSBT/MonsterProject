using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
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
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].transform.LookAt(Cam.transform);
            childs[i].rotation = Quaternion.Euler(0f, childs[i].transform.rotation.eulerAngles.y, 0f);

        }
    }
}
