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
    private float interactRange = 2f;
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
    private GameObject cageObj;
    
    [SerializeField]
    private Material outlineM;
    [SerializeField]
    private Material outlineA;
    private bool pushing = false;
    int[] puzzleList;
    private bool runing = false;

    private FormChanger fc;
    [SerializeField]
    public SoundManager soundManager;
    public Material OutLineA { get { return outlineA; } }
    public HungerBar HBar { get { return hBar; } set { hBar = value; } }
    public Transform CagePos { get { return cagePos; } }
    public GameObject CageObj {get { return cageObj; } }
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

    public bool OutOfCage { get { return outOfCage; } set { outOfCage = value;}}


    [Header("Tutorials")]
    [SerializeField]
    private GameObject tutorialObj;

    //cage variables
    private bool destroyCage = false;
    private bool outOfCage = true;
    
    private sceneManager _sceneManager;


    private void Awake()
    {

        //rb
        rb = GetComponent<Rigidbody>();
        hBar = GameObject.FindGameObjectWithTag("HunBar").GetComponent<HungerBar>();
        //set some value
        cagePos = GameObject.FindGameObjectWithTag("CagePos").transform;
        cageObj = GameObject.FindGameObjectWithTag("Cage");

        animator = GetComponentInChildren<Animator>();
        Input = new InputPlayerControl();
        Input.PlayerInput.Enable();
        Input.SystemInput.Enable();
        // some visual obj
        Interacticon = GameObject.Find("InteractIcon");
        //state  should be the last 
        states = new PlayerState(this);
        currentState = states.MoveState();
        currentState.EnterState();
        tutorialObj = GameObject.FindGameObjectWithTag("Tutorial");
        fc = GetComponentInChildren<FormChanger>();

        _sceneManager = GameObject.Find("SceneManager").GetComponent<sceneManager>();
    }
    private void Start()
    {

        tutorialObj.SetActive(false);
        firstSpawn();

        Input.SystemInput.Escape.performed += _sceneManager.quitGame;
        Input.SystemInput.Restart.performed += _sceneManager.restartGame;
    }

    public void firstSpawn()
    {
        cageObj.GetComponent<Animator>().SetBool("Open", false);
        transform.position = cagePos.position;
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
        if(outOfCage) {cageDisappear();}
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
            else if (OutlinedTarget.transform.parent.tag == "House")
            {
                InteractIcon.transform.position = new Vector3(0, 100f, 0);
            }
            else if (OutlinedTarget.transform.parent.tag == "Cage")
            {
                InteractIcon.transform.position = new Vector3(0, 100f, 0);
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
        Vector3 offset = new Vector3(0, 1, 0);
        Vector3 playerPos = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(playerPos + offset, interactRange);
        //Gizmos.DrawSphere(cageObj.transform.position , 2f);
    }

    //show tutorials
    public void showTutorial(string animatorParam , bool setParam)
    {
        tutorialObj.SetActive(setParam);
        tutorialObj.GetComponentInChildren<Animator>().SetBool(animatorParam , setParam);
    }

    public void cageDisappear() //Called in FixedUpdate
    {
        //Check Distance from cage and plays the disappear animation
        if(Vector3.Distance(transform.position , cageObj.transform.position) > 2f)
        {
            cageObj.GetComponent<Animator>().SetBool("destroyCage" , true);
            outOfCage = false; //Reset Value
        }

    }
}
