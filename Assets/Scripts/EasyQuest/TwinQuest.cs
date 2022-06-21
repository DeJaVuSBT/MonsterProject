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
    private void Update()
    {
        if (collected&& !complete)
        {
            //animation
            animator.SetBool("Complete", true);
            animator1.SetBool("Complete", true);
            box.SetActive(false);
            candyBag.SetActive(false);
            TimerAction.Create(() => Reset(), 50f);
            complete = true;
        }
    }

    private void Reset()
    {
        candyBag.SetActive(true);
        collected = false;
        complete = false;
        //animation
        animator.SetBool("Complete", false);
        animator1.SetBool("Complete", false);
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
