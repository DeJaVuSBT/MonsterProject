using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Animation : MonoBehaviour
{
    private GameObject mainCanvas;
    public GameObject foodPop;

    void Start()
    {
        mainCanvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FoodAnim();
        }

        void FoodAnim()
        {
            Instantiate(foodPop, mainCanvas.transform.position, Quaternion.identity);
        }
    }
}
