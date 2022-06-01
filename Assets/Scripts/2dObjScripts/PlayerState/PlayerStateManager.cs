using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private float moveSpeed ;
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
    public GameObject Target {
        get { return target; }
        set { target = value; }
    }
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
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 1, 0.7f);
        Vector3 playerPos = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(playerPos + offset, interactRange);
    }
}
