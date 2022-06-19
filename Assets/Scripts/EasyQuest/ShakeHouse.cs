using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeHouse : MonoBehaviour
{
    int counter;
    [SerializeField]
    Animator animator;
    bool reset=false;
    GameObject box;
    public int Counter { get { return counter; } set { counter = value; } }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter>=5&&reset==false)
        {
            //animator selebrate


            TimerAction.Create(() => counter = 0, 50);
            reset = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            //show box
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //hide box
        }
    }
}
