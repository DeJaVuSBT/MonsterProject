using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Character3daxis : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField]
    private Animator animator;
    private Rigidbody rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [Header("Property")]
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float interactRange = 1f;
    private Vector3 moveDir;
    [Header("Interaction")]
    [SerializeField]
    private GameObject target = null;

    //input
    InputPlayerControl Input;

    //interaction
    int difficulty;
    int phase;
    int currentInput;
    int[] puzzleList;
    bool pressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Input = new InputPlayerControl();
        Input.PlayerInput.Enable();
        //Input.EventInput.Enable();
        Input.EventInput.Button5.started+= Button5_started;
        Input.EventInput.Button4.started += Button4_started;
        Input.EventInput.Button3.started += Button3_started;
        Input.EventInput.Button2.started += Button2_started;
        Input.EventInput.Button1.started += Button1_started;
        Input.EventInput.AllKey.canceled += AllKey_canceled;
        
    }

    private void AllKey_canceled(InputAction.CallbackContext obj)
    {
        if (CheckIfInputCorrect())
        {
            phase++;
            Debug.Log("Good");
            if (phase>=puzzleList.Length)
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

    private void Button5_started(InputAction.CallbackContext obj) {
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


    private void Movement()
    {
        if (Input.PlayerInput.enabled)
        {
            Vector2 moveVector = Input.PlayerInput.Movement.ReadValue<Vector2>();
            moveDir = new Vector3(moveVector.x, 0, moveVector.y);
            rb.velocity = moveDir * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        Movement();
        //   Animation();
        Interact();
    }
    #region Interaction
    private GameObject ClosedColliderAround(){
        Vector3 playerPos = transform.position;
        Vector3 playerPosH = new Vector3(transform.position.x, 0.7f, transform.position.y);

        Collider[] ColliderAround = Physics.OverlapCapsule(playerPos, playerPosH, interactRange);

        if (ColliderAround.Length > 1)
        {
            Collider Closest = null;
            for (int i = 0; i < ColliderAround.Length; i++)
            {
                if (ColliderAround[i].gameObject==this.gameObject)
                {
                    continue;
                }
                if (Closest == null)
                {
                    Closest = ColliderAround[i];
                    continue;
                }
                if (Vector3.Distance(playerPos, Closest.transform.position) > Vector3.Distance(playerPos, ColliderAround[i].transform.position))
                {
                    Closest = ColliderAround[i];
                }
            }
            return Closest.gameObject;
        }
       
        return default;
    }
    private void Interact() {
        target = ClosedColliderAround();
        if (target != null&& target.GetComponent<Interactable>()!=null)
        {
            if (Input.PlayerInput.Interact.IsPressed())
            {
              target.GetComponent<Interactable>().Interact();
                //  CinematicBars.Show_Static(400, 0.3f);
                    RandomInputPuzzle();
            }

        }
    }
    #endregion
    /*
    private void Animation() {

        if (moveDir != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            oldX = moveX;
            oldZ = moveZ;
        }
        else { animator.SetBool("IsMoving", false); }
        animator.SetFloat("InputX", oldX);
        animator.SetFloat("InputY", oldZ);
    }
    */

    private void RandomInputPuzzle()
    {
        SwitchToEventInput();
        //difficulty 
        difficulty =  target.GetComponent<MoraEvents>().GetDifficulty();
        //make the puzzle
        puzzleList = new int[difficulty];
        for (int i = 0; i < difficulty; i++)
        {
            puzzleList[i] = UnityEngine.Random.Range(1, 6);
            Debug.Log(puzzleList[i]);
        }
        phase = 0;
    }
    private void SwitchToEventInput() { 
        Input.PlayerInput.Disable();
        Input.EventInput.Enable();
    }
    private void SwitchToPlayerInput()
    {
        Input.PlayerInput.Enable();
        Input.EventInput.Disable();
    }

}
