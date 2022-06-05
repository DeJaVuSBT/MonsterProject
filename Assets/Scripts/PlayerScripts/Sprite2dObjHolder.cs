using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite2dObjHolder : MonoBehaviour
{
    private Transform[] childs;
    [SerializeField]
    private Material depthShader;
    [SerializeField]
    private bool testShader;
    [SerializeField]
    private List<SpriteRenderer> renderers;
    // Start is called before the first frame update
    void Start()
    {
        //2d static setting
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
        //45 look up
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].rotation = Camera.main.transform.rotation;
        }

        //2d movable setting not in holder
        GameObject[] list = GameObject.FindGameObjectsWithTag("Moveable");
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].GetComponent<SpriteRenderer>())
            {
                renderers.Add(list[i].GetComponent<SpriteRenderer>());
                Preset(list[i].GetComponent<SpriteRenderer>());
            }
            else
            {
                renderers.Add(list[i].transform.GetChild(0).GetComponent<SpriteRenderer>());
                Preset(list[i].transform.GetChild(0).GetComponent<SpriteRenderer>());
            }

        }
      /*
        Transform a = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1);
        for (int i = 0; i < a.childCount; i++)
        {
            if (a.GetChild(i).GetComponent<SpriteRenderer>())
            {
                renderers.Add(a.GetChild(i).GetComponent<SpriteRenderer>());
            }
        }
      */
    

    }

    void Preset(SpriteRenderer sr) { 
        sr.material = depthShader;
        sr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        sr.sortingOrder=(int)(sr.gameObject.transform.position.z*-200);
       
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].sortingOrder = (int)(renderers[i].gameObject.transform.position.z * -200);
        }
    }
}
