using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerStateManager : MonoBehaviour
{
    //state
    PlayerBaseState currentState;
    PlayerState states;

    HungerBar hBar;
    [Header("Animation")]
    [SerializeField]
    private Animator animator;
   
    [Header("Movement")]
    private Rigidbody rb;
    private float moveSpeed;
    [SerializeField]
    private float runSpeed = 8f;
    [SerializeField]
    private float walkSpeed = 4f;
   
    [SerializeField]
    private float interactRange = 1f;
    private Vector3 moveDir;
    
    [Header("Input")]
    InputPlayerControl Input;
    
    [Header("Interaction")]
    [SerializeField]
    private GameObject target = null;
    private bool switchedTarget = false;
    private GameObject OutlinedTarget;
    private GameObject Interacticon;
    [SerializeField]
    private Transform cagePos;
    
    [SerializeField]
    private Material outlineM;
    [SerializeField]
    private Material outlineA;
    private bool pushing = false;
    int[] puzzleList;
    private bool runing = false;

    private FormChanger fc;

    public Material OutLineA { get { return outlineA; } }
    public HungerBar HBar { get { return hBar; } set { hBar = value; } }
    public Transform CagePos { get { return cagePos; } }
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Animator Animator { get { return animator; } set { animator = value; } }
    public Rigidbody RB { get { return rb; } set { rb = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float GetInteractRange { get { return interactRange; } }
    public Vector3 MoveDir { get { return moveDir; } set { moveDir = value; } }
    public GameObject Target { get { return target; } set { target = value; } }
    public InputPlayerControl InPut { get { return Input; } }
    public GameObject InteractIcon { get { return Interacticon; } }
    public Material GetOutLineMaterial { get { return outlineM; } }
    public bool Running { get { return runing; } set { runing = value; } }
    public int[] PuzzleList { get { return puzzleList; } set { puzzleList = value; } }
    public bool Pushing { get { return pushing; } set { pushing = value; } }

    public FormChanger Getfc { get { return fc; } set { fc = value; } }

    [Header("Tutorials")]
    [SerializeField]
    private GameObject tutorialObj;
    
    private void Awake()
    {

        //rb
        rb = GetComponent<Rigidbody>();
        hBar = GameObject.FindGameObjectWithTag("HunBar").GetComponent<HungerBar>();
        //set some value
        cagePos = GameObject.FindGameObjectWithTag("CagePos").transform;
        animator = GetComponentInChildren<Animator>();
        Input = new InputPlayerControl();
        Input.PlayerInput.Enable();
        // some visual obj
        Interacticon = GameObject.Find("InteractIcon");
        //state  should be the last 
        states = new PlayerState(this);
        currentState = states.MoveState();
        currentState.EnterState();

        tutorialObj = GameObject.FindGameObjectWithTag("Tutorial");
        fc = GetComponentInChildren<FormChanger>();

    }
    private void Start()
    {

        tutorialObj.SetActive(false);
        
    }

    public void SwitchToEventInput()
    {
        Input.PlayerInput.Disable();
        Input.EventInput.Enable();
    }
    public void SwitchToPlayerInput()
    {
        Input.PlayerInput.Enable();
        Input.EventInput.Disable();
    }

    public void SwitchToWaitState()
    {
        Input.PlayerInput.Disable();
        Input.EventInput.Disable();
    }

    public void ApplyRunSpeed() {
        moveSpeed = runSpeed;
    }
    public void ApplyWalkSpeed()
    {
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        currentState.UpdateState();
        flipSprite();
        ShowOutLine();

    }
    public void BeingCaught() {
        currentState.ExitState();
        currentState = states.PassOutState();
        currentState.EnterState();
    }
    private void flipSprite()
    {
        if (pushing)
        {
            //maybe something
        }
        else
        {
            if (moveDir.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                if (fc.SetType== FormChanger.UnitType.NL)
                {
                    fc.SetType = FormChanger.UnitType.NR;
                }
                
            }
            else if (moveDir.x == 0)
            {

            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                if (fc.SetType == FormChanger.UnitType.NR)
                {
                    fc.SetType = FormChanger.UnitType.NL;
                }
               
            }
        }

    }

    private void ShowOutLine() {
        if (Target!=null&& switchedTarget)
        {
            GameObject copyfromtarget = Target.GetComponentInChildren<MeshRenderer>().gameObject;
            GameObject CreatedoutlineObject = Instantiate(copyfromtarget, Target.transform);
            Debug.Log("created one outline");
            CreatedoutlineObject.GetComponent<Renderer>().material = outlineM;
            CreatedoutlineObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;

            if (OutlinedTarget==null)
            {
                OutlinedTarget = CreatedoutlineObject;
            }
            else
            {
                DestoryOutLinedTarget();
                OutlinedTarget = CreatedoutlineObject;
            }
            InteractIcon.SetActive(true);
            ///delete later only for test
            if (OutlinedTarget.transform.parent.tag == "temporary")
            {
                InteractIcon.transform.position = OutlinedTarget.transform.parent.position + new Vector3(-0.5f, 0.5f, 0.5f);
            }
            else { InteractIcon.transform.position = OutlinedTarget.transform.parent.position + new Vector3(-0.5f, 2, 0.5f); }


            switchedTarget = false;
        }
        
    }
    public void DestoryOutLinedTarget() {
        if (OutlinedTarget)
        {
            Destroy(OutlinedTarget);
            InteractIcon.SetActive(false);
        }
    
    }
    public void SwitchedTarget() {
        switchedTarget = true;
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 1, 0.7f);
        Vector3 playerPos = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(playerPos + offset, interactRange);
    }

    //show tutorials
    public void showTutorial(string animatorParam , bool setParam)
    {
        tutorialObj.SetActive(setParam);
        tutorialObj.GetComponentInChildren<Animator>().SetBool(animatorParam , setParam);
    }
}
