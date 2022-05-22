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
    [SerializeField]
    private int maxDifficulty = 3;
    private bool isInteracting = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public int GetDifficulty() {
        return maxDifficulty;
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
        isInteracting = false;
    }

    public void Interact()
    {
        isInteracting = true;
    }
    private void Visual() {
        this.gameObject.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f)); ;

    }
    void Update() {
        if (isInteracting)
        {
            Visual();
        }
    }

}
