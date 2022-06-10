using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeedSwitch : MonoBehaviour
{
    List<GameObject> cardList = new List<GameObject>();
    [SerializeField]
    private GameObject badP, generalP, goodP;
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            cardList.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
