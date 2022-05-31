using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    //state
    PlayerBaseState currentState;
    PlayerState states;
    public PlayerBaseState CurrentState{
        get { return currentState; }
        set { currentState = value; }
    }
    [Header("Animation")]
    [SerializeField]
    private Animator animator;
    [Header("Movement")]
    private Rigidbody rb;
    public Rigidbody RB { get { return rb; } set { rb = value; } }

    private float moveSpeed = 4f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    [SerializeField]
    private float interactRange = 1f;
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
    int phase;
    int currentInput;
    int[] puzzleList;
    bool runing;
    private void Awake()
    {
        //state
        states= new PlayerState(this);
        currentState = states.MoveState();
        currentState.EnterState();

        //rb
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        Input = new InputPlayerControl();
        Input.PlayerInput.Enable();
        //Input.EventInput.Enable();

        Input.PlayerInput.Interact.performed += Interact_performed;
        Input.PlayerInput.Interact.canceled += Interact_canceled;
        //key puzzle event
        Input.EventInput.Button5.started += Button5_started;
        Input.EventInput.Button4.started += Button4_started;
        Input.EventInput.Button3.started += Button3_started;
        Input.EventInput.Button2.started += Button2_started;
        Input.EventInput.Button1.started += Button1_started;
        Input.EventInput.AllKey.canceled += AllKey_canceled;
        Input.EventInput.AllKey.started += AllKey_started;
        //push event
        Input.PushInput.Interact.canceled += Push_canceled;

    }

    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        runing = false;
    }
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        runing = true;
    }

    private void Push_canceled(InputAction.CallbackContext obj)
    {
        SwitchToPlayerInput();
    }

    private void AllKey_canceled(InputAction.CallbackContext obj)
    {
        if (CheckIfInputCorrect())
        {
            phase++;
            Debug.Log("Good");
            if (phase >= puzzleList.Length)
            {
                target.GetComponent<Reward>().Reward();
                SwitchToPlayerInput();
                Debug.Log("Passed");
            }
        }
        else
        {
            SwitchToPlayerInput();
            Debug.Log("failed");
        }

    }
    private void AllKey_started(InputAction.CallbackContext obj)
    {
        target.GetComponent<MoraEvents>().Shake();

    }

    private void Button5_started(InputAction.CallbackContext obj)
    {
        currentInput = 5;
        Debug.Log("Space pressed");
    }
    private void Button4_started(InputAction.CallbackContext obj)
    {
        currentInput = 4;
        Debug.Log("right");
    }
    private void Button3_started(InputAction.CallbackContext obj)
    {
        currentInput = 3;
        Debug.Log("left");
    }
    private void Button2_started(InputAction.CallbackContext obj)
    {
        currentInput = 2;
        Debug.Log("down");
    }
    private void Button1_started(InputAction.CallbackContext obj)
    {
        currentInput = 1;
        Debug.Log("up");
    }
    private bool CheckIfInputCorrect()
    {
        return currentInput == puzzleList[phase] ? true : false;
    }
    private void SwitchToEventInput()
    {
        Input.PlayerInput.Disable();
        Input.EventInput.Enable();
        Input.PushInput.Disable();
        Debug.Log("Event");
    }
    private void SwitchToPushInput()
    {
        Input.PushInput.Enable();
        Input.PlayerInput.Disable();
        Input.EventInput.Disable();
        Debug.Log("PushEvent");
    }
    private void SwitchToPlayerInput()
    {
        Input.PlayerInput.Enable();
        Input.EventInput.Disable();
        Input.PushInput.Disable();
        Debug.Log("player normal input");
    }
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }


}
