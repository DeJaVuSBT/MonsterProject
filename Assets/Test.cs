using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject one, two,three;
    public int Switch;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (Switch)
        { 
            case 0:
        
            one.SetActive(true);
            two.SetActive(false);
                three.SetActive(false);
                break;
        case 1:
        
            one.SetActive(false);
            two.SetActive(true);
                three.SetActive(false);
                break;
            default:
                one.SetActive(false);
                two.SetActive(false);
                three.SetActive(true);

                break;

       }
    }
}
