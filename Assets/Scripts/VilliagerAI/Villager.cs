using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    [Header("AI Setting")]
    [SerializeField]
    [Range(0, 5)]
    private float senseRange = 4f;
    [SerializeField]
    [Range(0,3)]
    private float roamingDis=2f;
    [SerializeField]
    [Range(0, 3)]
    private float moveSpeed;
    [SerializeField]
    [Range(0, 3)]
    private float pushMoveSpeed=0.5f;
    [SerializeField]
    [Range(0, 3)]
    private float walkMoveSpeed=1f;
    [SerializeField]
    [Range(0, 3)]
    private float runMoveSpeed=2.5f;
    [SerializeField]
    private float pushDis=1f;
    [SerializeField]
    private int safeDis=6;
    private Vector3 startPos;
    private Vector3 roamingDestination;
    private Rigidbody rb;
    private GameObject player;
    [SerializeField]
    private MoralityBar mBar;
    [SerializeField]
    private State state;
    private bool pushable=false;

    private Transform attacker;     // 起始点
    private Vector3 middlePoint;   // 中间点
    public float heightOffSet=2f;
    private Transform playerHeadPos;      // 终止点
    public GameObject stonePrefab;       // 要移动的物体
    private GameObject stone;

    private float ticker = 0.0f;
    [SerializeField]
    private float attackTime = 0.5f;    // 假设要用2秒飞到目标点
    void Start()
    {
        startPos = transform.position;
        roamingDestination = GetRoamingPos();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        state = State.Roaming;
        moveSpeed = walkMoveSpeed;
        attacker = this.transform.GetChild(1);
        playerHeadPos = player.transform.GetChild(0);
        stone = Instantiate(stonePrefab);
        stone.SetActive(false);
    }
    private enum State { 
        Roaming,
        ChasePlayer,
        PushPlayer,
        ThrowPlayer,
        Flee,
        StandStillAngry
    }
    // Update is called once per frame
    void Update()
    {
        switch (state) {
            default:
            case State.Roaming:
                Roaming();
                SensePlayer();
                break;
            case State.ChasePlayer:
                ChasePlayer();
                break;
            case State.ThrowPlayer:
                ThrowPlayer();
                break;
            case State.Flee:
                Flee();
                break;
            case State.StandStillAngry:
                Vigilant();
                break;
        }
    }

    private void ThrowPlayer()
    {
        stone.SetActive(true);
        ticker += Time.deltaTime;
        float t = ticker / attackTime;  
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        Vector3 p1 = attacker.position;
        Vector3 p3 = playerHeadPos.position;
        middlePoint = (p3 - p1) / 2 + p1 + new Vector3(0, heightOffSet, 0);
        Vector3 p2 = middlePoint;
        Vector3 currPos = ThrowCurve.GetCurvePoint(p1, p2, p3, t);
        
        stone.transform.position = currPos;
 


        if (t == 1.0f)
        {
            Vector3 currPos1 = ThrowCurve.GetCurvePoint(p1, p2, p3, t);
            Vector3 currPos2 = ThrowCurve.GetCurvePoint(p1, p2, p3, t-0.1f);
            Vector3 stoneEndVelocity = 10 * (currPos1 - currPos2);
            stone.GetComponent<Rigidbody>().velocity = stoneEndVelocity;

            ticker = 0;
            state = State.Flee;
        }
    }

    void Vigilant() { 
        
    }


    void SensePlayer() {
        if (Vector3.Distance(transform.position,player.transform.position)< senseRange)
        {
            if (mBar.GetMoralAmount()<=50)
            {
                state = State.ChasePlayer;
            }
            else if (mBar.GetMoralAmount() > 50&& mBar.GetMoralAmount() <= 75)
            {
                state = State.Flee;
            }
            else if(mBar.GetMoralAmount() < 75)
            {
                state = State.Roaming;
            }
        }
    
    }


    void ChasePlayer()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            rb.velocity = Vector3.zero ;
         //   if (pushable)
          //  {
                state = State.ThrowPlayer;
         //   }
            
         //   else
          //  {
             //   MoveTo(player.transform.position);
          //  }
        }
        else
        {
            moveSpeed = runMoveSpeed;
            MoveTo(player.transform.position);
        }
    }

    private void PushPlayer() {

        player.transform.position -= pushDis * -GetDirXZ(player.transform.position);



        state = State.Flee;
    }
    
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            pushable = true;
        }
    }
    */
    void Roaming() {
        MoveTo(roamingDestination);
        if (Vector3.Distance(transform.position, roamingDestination) <0.2f)
        {
            //when villager get the point get new destination 
            roamingDestination = GetRoamingPos();
        }
    }

    void Flee() {
        
        moveSpeed = runMoveSpeed;
        if (Vector3.Distance(transform.position, player.transform.position) > safeDis)
        {
            stone.SetActive(false);
            moveSpeed = walkMoveSpeed;
            roamingDestination = GetRoamingPos();
            state = State.Roaming;
        }
        else
        {
            FleeFrom(player.transform.position);
        }
    }

    void FleeFrom(Vector3 targetPos) {
        rb.velocity = -GetDirXZ(targetPos) * moveSpeed;
    }
    void MoveTo(Vector3 targetPos) { 
        
        rb.velocity = GetDirXZ(targetPos)* moveSpeed;
    }

    Vector3 GetDirXZ(Vector3 targetPos) { 
        return (targetPos-transform.position).normalized;
    }


    Vector3 GetNowPos() {
        
        return transform.position;
    }
    Vector3 GetRoamingPos() {
        return GetRandomDir() * roamingDis + GetNowPos();
    }

    Vector3 GetRandomDir() { 
        return new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized;
    }
}
