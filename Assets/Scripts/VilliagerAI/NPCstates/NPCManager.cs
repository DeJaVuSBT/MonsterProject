using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    //state
    NPCStateBase currentState;
    NPCStates states;
    //obj/component
    private GameObject player;
    private MoralityBar mBar;
    private Rigidbody rb;
    private NavMeshAgent agent;
    //Setting
    [SerializeField]
    private float senseRange=6;
    [SerializeField]
    private float roamingRange=6;
    private Vector3 originalPos;
    [SerializeField]
    private const int lowMoral=20;

    [SerializeField]
    private const int highMoral = 75;

    [SerializeField]
    private Sprite[] emojiList;
    [SerializeField]
    private SpriteRenderer emojiHolder;





    //get set
    public float RoamingRange { get { return roamingRange; } }
    public Vector3 OriginalPos { get { return originalPos; } }
    public NavMeshAgent Agent { get { return agent; } set { agent = value; } }
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
        agent = GetComponent<NavMeshAgent>();
        //initial state
        originalPos = transform.position;
        states = new NPCStates(this);
        currentState = states.Idle();
        currentState.EnterState();
    }
    public void ShowEmotion(int emoji) {
        emojiHolder.sprite = emojiList[emoji];
        emojiHolder.gameObject.SetActive(true);
        TimerAction.Create(() => emojiHolder.gameObject.SetActive(false), 2f);
    }
    void Update()
    {
        currentState.UpdateState();
    }
}
