using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraEvents : MonoBehaviour, Interactable,Reward
{
    [SerializeField]
    private bool badOrGood;
    [SerializeField]
    private bool istriggered = false;
    public MoralityBar bar;
    Rigidbody2D rb;
    Animator animator;
    bool open=false;

    int maxDifficulty = 4;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void Reward()
    {
        if (!istriggered) {
            if (badOrGood)
            {
                bar.Add(10);
                
            }
            else
            {
                bar.Decrease(10);
            }
            istriggered = true;
        }
    }

    public void Interact()
    {
    //    Visual();
    
    }
    private void Visual() {
        if (!open)
        {
            animator.SetBool("Open", true);
            open = true;
        }
        else
        {
            animator.SetBool("Open", false);
            open = false;
        } 
    }

}
