using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
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
    private int safeDis=6;
    private Vector3 startPos;
    private Vector3 roamingDestination;
    private Rigidbody rb;
    [SerializeField]
    private Transform cage;
    private GameObject player;
    [SerializeField]
    private MoralityBar mBar;
    [SerializeField]
    private State state;
    private bool catchable = false;
    void Start()
    {
        startPos = transform.position;
        roamingDestination = GetRoamingPos();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        state = State.Roaming;
        moveSpeed = walkMoveSpeed;
    }
    private enum State { 
        Roaming,
        ChasePlayer,
        CatchPlayer,
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
            case State.CatchPlayer:
                CatchPlayer();
                break;
        }
    }

    private void CatchPlayer()
    {
        player.GetComponent<Character3daxis>().BeingCatched(true);
        player.transform.parent = (this.transform);
        moveSpeed = walkMoveSpeed;
        MoveTo(cage.position);
    }

    void SensePlayer() {
        if (Vector3.Distance(transform.position,player.transform.position)< senseRange)
        {
            if (mBar.GetMoralAmount()<=25)
            {
                state = State.ChasePlayer;
            }
            else if(mBar.GetMoralAmount() > 25)
            {
                state = State.Roaming;
            }
        }
    
    }


    void ChasePlayer()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            moveSpeed = pushMoveSpeed;
            if (catchable)
            {
                state = State.CatchPlayer;
            }
            
            else
            {
                MoveTo(player.transform.position);
            }
        }
        else
        {
            moveSpeed = runMoveSpeed;
            MoveTo(player.transform.position);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            catchable = true;
        }
    }

    void Roaming() {
        MoveTo(roamingDestination);
        if (Vector3.Distance(transform.position, roamingDestination) <0.2f)
        {
            //when villager get the point get new destination 
            roamingDestination = GetRoamingPos();
        }
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
