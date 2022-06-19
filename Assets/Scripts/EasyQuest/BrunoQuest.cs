using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrunoQuest : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    BoxCollider stoneCheckCollider;
    [SerializeField]
    GameObject box;
    private bool complete=false;

    private int counter;

    public int RockCount
    {
        get { return counter; }
        set { counter = value; }
    }


    private void Start()
    {
        RockCount = 4;
        box.SetActive(false);
    }
    private void Update()
    {
        if (RockCount==0&& !complete)
        {
            //animation

            box.SetActive(false);
            TimerAction.Create(() => Reset(), 50f);
            complete = true;
        }
    }

    private void Reset()
    {
        //rock back
        complete = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            box.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"&& complete == false)
        {
            box.SetActive(false);
        }
    }
}
