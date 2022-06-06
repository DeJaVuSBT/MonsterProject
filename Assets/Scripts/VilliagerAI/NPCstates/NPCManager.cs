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
    //Setting
    [SerializeField]
    private float senseRange;
    [SerializeField]
    private const int lowMoral=20;

    [SerializeField]
    private const int highMoral = 75;

    [SerializeField]
    private Sprite[] emojiList;
    [SerializeField]
    private SpriteRenderer emojiHolder;





    //get set
    public int LowMoral { get { return lowMoral; } }
    public int HighMoral { get { return highMoral; } }
    public float SeneseRange { get { return senseRange; } set { senseRange = value; } }
    public NPCStateBase CurrentState { get { return currentState; } set { currentState = value; } }
    public Rigidbody RB { get { return rb; } set { rb = value; } }
    public GameObject Player { get { return player; } }
    public MoralityBar MBar { get { return mBar; } set { mBar = value; } }
    
    void Start()
    {
        mBar = GameObject.FindGameObjectWithTag("MorBar").GetComponent<MoralityBar>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        emojiHolder.gameObject.SetActive(false);
        //initial state
        states = new NPCStates(this);
        currentState = states.Idle();
        currentState.EnterState();
    }

    void Update()
    {
        
    }
}
