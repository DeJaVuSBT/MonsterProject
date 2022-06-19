using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BozeQuest : MonoBehaviour
{
    int counter;
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject box;
    bool complete = false;
    public int Counter { get { return counter; } set { counter = value; } }
    void Start()
    {
        
    }

    private void Update()
    {
        if (counter >=5 && !complete)
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
        counter = 0;
        complete = false;
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
