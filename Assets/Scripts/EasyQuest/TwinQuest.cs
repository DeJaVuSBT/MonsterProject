using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinQuest : MonoBehaviour
{
    [SerializeField]
    GameObject candyBag,box,bagUI;
    [SerializeField]
    Animator animator,animator1;
    bool complete=false;
    bool collected = false;
    bool collectvisual = false;
    public bool GetCandy { set { collected = value; } }
    private GameObject bag;
    float ticker=0f;
    GameObject throwBag;
    private void Start()
    {
        box.SetActive(false);
        bag = Instantiate(bagUI, GameObject.Find("CanvasFinal").transform);
        bag.SetActive(false);
        throwBag  = Instantiate(candyBag);
        throwBag.transform.position = new Vector3(0, 100, 0);
    }
    private void Update()
    {
        if (collected&& !collectvisual)
        {
            box.SetActive(false);
            candyBag.SetActive(false);
            bag.SetActive(true);
            collectvisual = true;
        }
        if (complete)
        {
            
            ticker += Time.deltaTime;
            float t = ticker / 2f;
            t = Mathf.Clamp(t, 0.0f, 1.0f);
            Vector3 p1 = GameObject.FindGameObjectWithTag("Player").transform.position+Vector3.up*2;
            Vector3 p3 = this.transform.position + Vector3.up * 2;
            Vector3 p2 = (p3 - p1) / 2 + p1 + new Vector3(0, 2f, 0);

            Vector3 currPos = ThrowCurve.GetCurvePoint(p1, p2, p3, t);
            throwBag.transform.position = currPos;

            if (t >= 1.0f)
            {
                TimerAction.Create(() => animator.SetBool("Complete", true), 0.5f);
                animator1.SetBool("Complete", true);
                complete = false;
                TimerAction.Create(() => Reset(), 10f);
                throwBag.transform.position = new Vector3(0, 100, 0);
            }
        }
      
    }

    private void Reset()
    {
        candyBag.SetActive(true);
        collected = false;
        complete = false;
        collectvisual = false;
        //animation
        animator.SetBool("Complete", false);
        animator1.SetBool("Complete", false);
       
        box.SetActive(true);
        ticker = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if (collected)
            {
                bag.SetActive(false);
                complete = true;
            }
            else
            {
                box.SetActive(true);
            }
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
