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
    public PlayerBaseState CurrentState {
        get { return currentState; }
        set { currentState = value; }
    }
    [Header("Animation")]
    [SerializeField]
    private Animator animator;
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }
    [Header("Movement")]
    private Rigidbody rb;
    public Rigidbody RB { get { return rb; } set { rb = value; } }

    private float moveSpeed;
    [SerializeField]
    private float runSpeed = 8f;
    [SerializeField]
    private float walkSpeed = 4f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    [SerializeField]
    private float interactRange = 1f;
    public float GetInteractRange {
        get { return interactRange; }
    }
    private Vector3 moveDir;
    public Vector3 MoveDir {
        get { return moveDir; }
        set { moveDir = value; }
    }

    [Header("Input")]
    InputPlayerControl Input;
    public InputPlayerControl InPut
    {
        get { return Input; }

    }
    [Header("Interaction")]
    [SerializeField]
    private GameObject target = null;
    private bool switchedTarget = false;
    private GameObject OutlinedTarget;
    private GameObject Interacticon;
    public GameObject InteractIcon { get { return Interacticon; } }
    public GameObject Target {
        get { return target; }
        set { target = value; }
    }
    [SerializeField]
    private Material outlineM;
    public Material GetOutLineMaterial { get { return outlineM; } }

    [Header("UI Tutorials")]
    [SerializeField]
    bool tutorialIsOn = false;
    int tutSelector;
    public GameObject[] tutorials;

    private bool pushing=false;
    public bool Pushing {
        get { return pushing; }
        set { pushing = value; }
    }
    int currentInput;
    int[] puzzleList;
    public int[] PuzzleList
    {
        get { return puzzleList; }
        set { puzzleList = value; }
    }
    private bool runing=false;
    public bool Running { get { return runing; } set { runing = value; } }
    private void Awake()
    {
        //state
        states= new PlayerState(this);
        currentState = states.MoveState();
        currentState.EnterState();
        
        //rb
        rb = GetComponent<Rigidbody>();
        //set some value
        
        animator = GetComponentInChildren<Animator>();
        Input = new InputPlayerControl();
        Input.PlayerInput.Enable();
        // some visual obj
        Interacticon = GameObject.Find("InteractIcon");

    }

    private void Push_canceled(InputAction.CallbackContext obj)
    {
        SwitchToPlayerInput();
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

    public void ApplyRunSpeed() {
        moveSpeed = runSpeed;
    }
    public void ApplyWalkSpeed()
    {
        moveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
      //  UpdateRenderOrder();
        ShowOutLine();
        //Debug.Log(currentState);
        //tutorialLogic();
    }
    
    private void ShowOutLine() {
        if (Target!=null&& switchedTarget)
        {
            GameObject copyfromtarget = Target.GetComponentInChildren<MeshRenderer>().gameObject;
            GameObject CreatedoutlineObject = Instantiate(copyfromtarget, Target.transform);
            Debug.Log("created one outline");

            CreatedoutlineObject.GetComponent<Renderer>().material = outlineM;

            CreatedoutlineObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;

            //Maybe code from mark
            //     if (CreatedoutlineObject.GetComponent<SortingGroup>() != null)
            //     {
            //         CreatedoutlineObject.GetComponent<SortingGroup>().sortingOrder = Target.GetComponentInChildren<SortingGroup>().sortingOrder - 1;
            //     }
     

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
                InteractIcon.transform.position = OutlinedTarget.transform.parent.position + new Vector3(0, 0.5f, 0);
                Debug.Log("bush");
            }
            else { InteractIcon.transform.position = OutlinedTarget.transform.parent.position + new Vector3(0.5f, 2, 0.5f); }


            switchedTarget = false;
        }
        
    }
    //code from mark
  //  private void UpdateRenderOrder()
  //  {
  //      if (Target != null && Target.GetComponentInChildren<SortingGroup>() != null)
  //      {
  //          if (transform.position.z < Target.transform.position.z)
  //          {
  //              Target.GetComponentInChildren<SortingGroup>().sortingOrder = 1;
  //          }
  //          else
  //          {
  //              Target.GetComponentInChildren<SortingGroup>().sortingOrder = 20;
  //          }
  //      }
  //  }

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

    //UI Tutorials
    public void startTutorial(int tutIndex){
        GameObject tutorialHolder = tutorials[tutIndex];
        tutorialHolder.SetActive(true);
    }

    public void endTutorial(int tutIndex){
        GameObject tutorialHolder = tutorials[tutIndex];
        tutorialHolder.SetActive(false);
        tutorialIsOn = false;
    }

    void tutorialLogic()
    {
        //Debug.Log(CurrentState);
        if(currentState == states.ShakeState())
        {
            startTutorial(0);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 1, 0.7f);
        Vector3 playerPos = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(playerPos + offset, interactRange);
    }
}
