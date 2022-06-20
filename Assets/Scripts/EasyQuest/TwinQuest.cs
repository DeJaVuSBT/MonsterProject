using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinQuest : MonoBehaviour
{
    [SerializeField]
    GameObject candyBag,box;
    [SerializeField]
    Animator animator,animator1;
    bool complete=false;
    bool collected = false;
    public bool GetCandy { set { collected = value; } }
    void Start()
    {
        
    }

    private void Update()
    {
        if (collected&& !complete)
        {
            //animation

            box.SetActive(false);
            candyBag.SetActive(false);
            TimerAction.Create(() => Reset(), 50f);
            complete = true;
        }
    }

    private void Reset()
    {
        //rock back
        candyBag.SetActive(true);
        complete = false;
        //animation
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            box.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && complete == false)
        {
            box.SetActive(false);
        }
    }
}
