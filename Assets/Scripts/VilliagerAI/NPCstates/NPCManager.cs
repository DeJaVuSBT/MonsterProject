using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    //state
    NPCStateBase currentState;
    NPCStates states;
    //obj
    private GameObject player;
    private MoralityBar mBar;
    private Rigidbody rb;

    //get set

    public NPCStateBase CurrentState { get { return currentState; } set { currentState = value; } }
    public Rigidbody RB { get { return rb; } set { rb = value; } }
    public GameObject Player { get { return player; } }
    public MoralityBar MBar { get { return mBar; } set { mBar = value; } }

    void Start()
    {
        mBar = GameObject.FindGameObjectWithTag("MorBar").GetComponent<MoralityBar>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
    }
}
