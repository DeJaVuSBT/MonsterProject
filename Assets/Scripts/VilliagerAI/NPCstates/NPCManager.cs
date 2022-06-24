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
    private DeedSwitch mBar;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Animator animator;
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
    //throwstone setup
    public GameObject stonePrefab;
    private GameObject stone;
    float preX;
    //get set
    public GameObject Stone { get { return stone; } set { stone = value; } }
    public Animator Animator { get { return animator; } set { animator = value; } }
    public float RoamingRange { get { return roamingRange; } }
    public Vector3 OriginalPos { get { return originalPos; } }
    public NavMeshAgent Agent { get { return agent; } set { agent = value; } }
    public int LowMoral { get { return lowMoral; } }
    public int HighMoral { get { return highMoral; } }
    public float SeneseRange { get { return senseRange; } set { senseRange = value; } }
    public NPCStateBase CurrentState { get { return currentState; } set { currentState = value; } }
    public Rigidbody RB { get { return rb; } set { rb = value; } }
    public GameObject Player { get { return player; } }
    public DeedSwitch MBar { get { return mBar; } set { mBar = value; } }
    
    void Start()
    {
        mBar = GameObject.FindGameObjectWithTag("Mbar").GetComponent<DeedSwitch>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (this.gameObject.tag=="MH")
        {
            stone = Instantiate(stonePrefab);
            stone.SetActive(false);
        }
        //initial state
        originalPos = transform.position;
        states = new NPCStates(this);
        currentState = states.Idle();
        currentState.EnterState();
        preX = this.transform.position.x;
    
    }
    private void filpSprite()
    {
        if (left())
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private bool left() {
        

            if (agent.desiredVelocity.x < 0)
            {
                return true;
            }
            else
            {
                return false;
            }

    }

    void Update()
    {
        filpSprite();
        currentState.UpdateState();
    }
}
