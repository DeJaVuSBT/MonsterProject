using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("AI Setting")]
    [SerializeField]
    [Range(0, 5)]
    private float senseRange = 4f;
    [SerializeField]
    [Range(0, 3)]
    private float roamingDis = 2f;
    [SerializeField]
    [Range(0, 3)]
    private float moveSpeed;
    [SerializeField]
    [Range(0, 3)]
    private float pushMoveSpeed = 0.5f;
    [SerializeField]
    [Range(0, 3)]
    private float walkMoveSpeed = 1f;
    [SerializeField]
    [Range(0, 3)]
    private float runMoveSpeed = 2.5f;
    [SerializeField]
    private float pushDis = 1f;
    [SerializeField]
    private int safeDis = 6;
    [SerializeField]
    private float moveRange = 6;
    private Vector3 startPos;
    private Vector3 roamingDestination;
    private Rigidbody rb;
    private GameObject player;
    [SerializeField]
    private MoralityBar mBar;
    [SerializeField]
    private State state;

    void Start()
    {
        startPos = transform.position;
        roamingDestination = GetRoamingPos();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        state = State.Roaming;
        moveSpeed = walkMoveSpeed;
        mBar = GameObject.FindGameObjectWithTag("MorBar").GetComponent<MoralityBar>();
    }
    private enum State
    {
        Roaming,
        ChasePlayer,
        Flee,
        GoBack
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                SensePlayer();
                break;
            case State.ChasePlayer:
                ChasePlayer();
                CheckOutRange();
                break;
            case State.Flee:
                Flee();
                break;
            case State.GoBack:
                GoBack();
                break;


        }
    }
    void GoBack() {
        if (Vector3.Distance(transform.position, startPos) > 1f)
        {
            MoveTo(startPos);
        }
        else
        {
            state = State.Roaming;
        }
    }


    void CheckOutRange() {
        if (Vector3.Distance(transform.position, startPos) > moveRange)
        {
           state = State.GoBack;
        }

    }
    void SensePlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < senseRange)
        {
            if (mBar.GetMoralAmount() <= 50)
            {
                state = State.Flee;
            }
            else if (mBar.GetMoralAmount() > 50 && mBar.GetMoralAmount() <= 60)
            {
                state = State.Roaming;
            }
            else if (mBar.GetMoralAmount() > 60)
            {
                state = State.ChasePlayer;
            }
        }
    }
 



        void ChasePlayer()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            moveSpeed = runMoveSpeed;
            MoveTo(player.transform.position);
        }
    }

    void Roaming()
    {
        MoveTo(roamingDestination);
        if (Vector3.Distance(transform.position, roamingDestination) < 0.2f)
        {
            //when villager get the point get new destination 
            roamingDestination = GetRoamingPos();
        }
    }

    void Flee()
    {

        moveSpeed = runMoveSpeed;
        if (Vector3.Distance(transform.position, player.transform.position) > safeDis)
        {
            moveSpeed = walkMoveSpeed;
            roamingDestination = GetRoamingPos();
            state = State.Roaming;
        }
        else
        {
            FleeFrom(player.transform.position);
        }
    }

    void FleeFrom(Vector3 targetPos)
    {
        rb.velocity = -GetDirXZ(targetPos) * moveSpeed;
    }
    void MoveTo(Vector3 targetPos)
    {

        rb.velocity = GetDirXZ(targetPos) * moveSpeed;
    }

    Vector3 GetDirXZ(Vector3 targetPos)
    {
        return (targetPos - transform.position).normalized;
    }

    Vector3 GetRoamingPos()
    {
        return startPos+new Vector3(Random.Range(-moveRange/2, moveRange/2),0,Random.Range(-moveRange/2, moveRange/2));
    }

}
