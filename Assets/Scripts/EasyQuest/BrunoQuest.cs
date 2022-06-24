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
    [SerializeField]
    GameObject[] RocksList;
    Vector3[] RockPosList=new Vector3[4];

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
        for (int i = 0; i < RockPosList.Length; i++)
        {
            RockPosList[i] = RocksList[i].transform.position;
        }
    }
    private void Update()
    {
        if (RockCount==0&& !complete)
        {
            //animation
            animator.SetBool("Complete", true);
            box.SetActive(false);
            TimerAction.Create(() => Reset(), 5f);
            complete = true;
        }
    }

    private void Reset()
    {
        //rock back
        for (int i = 0; i < RocksList.Length; i++)
        {
            RocksList[i].transform.position = RockPosList[i];
        }


        RockCount = 4;
        complete = false;
        animator.SetBool("Complete", false);
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
