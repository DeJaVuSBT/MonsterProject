﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite2dObjHolder : MonoBehaviour
{
    private Transform[] childs;
    [SerializeField]
    private Material depthShader;
    [SerializeField]
    private bool testShader;
    // Start is called before the first frame update
    void Start()
    {
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
            if (childs[i].GetComponent<SpriteRenderer>())
            {
               // childs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
                if (testShader)
                {
                    Preset(childs[i].GetComponent<SpriteRenderer>());
                }
                
            }
            else if (!childs[i].GetComponent<SpriteRenderer>()&& childs[i].childCount!=0)
            {
                for (int j = 0; j < childs[i].childCount; j++)
                {
                    if (childs[i].GetChild(j).GetComponent<SpriteRenderer>())
                    {

                        Preset(childs[i].GetChild(j).GetComponent<SpriteRenderer>());
                    }
                }
            }
            
        }
    }

    void Preset(SpriteRenderer sr) { 
        sr.material = depthShader;
        sr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        sr.sortingOrder=(int)(sr.gameObject.transform.position.z*-200);
       
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
