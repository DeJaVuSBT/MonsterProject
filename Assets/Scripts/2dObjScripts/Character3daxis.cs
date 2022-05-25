using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.Utils;
public class Character3daxis : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField]
    private Animator animator;
    private Rigidbody rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    Transform middle;
    [Header("Property")]
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float interactRange = 1f;
    private Vector3 moveDir;
    [Header("Interaction")]
    [SerializeField]
    private GameObject target = null;
    [SerializeField]
    private float pushSpeed = 1f;

    //input
    InputPlayerControl Input;

    //interaction
    int difficulty;
    int phase;
    int currentInput;
    int[] puzzleList;
    bool runing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        Input = new InputPlayerControl();
        Input.PlayerInput.Enable();
        //Input.EventInput.Enable();

        Input.PlayerInput.Interact.performed += Interact_performed;
        Input.PlayerInput.Interact.canceled += Interact_canceled;
        //key puzzle event
        Input.EventInput.Button5.started+= Button5_started;
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
    private void AllKey_started(InputAction.CallbackContext obj)
    {
        target.GetComponent<MoraEvents>().Shake();

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
            if (runing)
            {
                moveSpeed = 8f;
            }
            else
            {
                moveSpeed = 4f;
            }
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
        Animation();
        Interact();
    }
    #region Interaction
    private GameObject ClosedColliderAround(){
        Vector3 playerPos = transform.position;
        //Vector3 playerhead = transform.position + new Vector3(0, 0.5f, -1f);
        Collider[] ColliderAround = Physics.OverlapSphere(middle.position, interactRange);

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
            return Closest.transform.gameObject;
        }
       
        return default;
    }
    private void Interact() {
        if (Input.PlayerInput.enabled&&!runing)
        {
            target = ClosedColliderAround();
        }
        if (target != null&& target.GetComponent<Interactable>()!=null)
        {
            if (Input.PlayerInput.Interact.IsPressed())
            {
               
                    target.GetComponent<Interactable>().Interact(); 
                //  CinematicBars.Show_Static(400, 0.3f);
                switch (target.GetComponent<MoraEvents>().GetInteractType())
                {
                    case 0:
                        UtilsClass.CreateWorldTextPopup("Shaking with " +target.ToString(), transform.position);
                        ShakeInput();
                        break;
                    case 1:
                        UtilsClass.CreateWorldTextPopup("Rotating with " + target.ToString(), transform.position);
                        RotateInput();
                        break;
                    case 2:
                        UtilsClass.CreateWorldTextPopup("Pusing  " + target.ToString(), transform.position);
                        PushInput();
                        break;
                    default:
                        UtilsClass.CreateWorldTextPopup("Shaking with  " + target.ToString(), transform.position);
                        ShakeInput();
                        break;
                }
                
            }

        }


        if (Input.PushInput.enabled)
        {
            Vector2 moveVector = Input.PushInput.Movement.ReadValue<Vector2>();
            moveDir = new Vector3(moveVector.x, 0, moveVector.y);
            transform.position += moveDir * pushSpeed * Time.deltaTime;
           // Vector3 oldPosInteractable = target.transform.position;
            target.transform.position += moveDir * pushSpeed * Time.deltaTime;
        }
    }
    #endregion
   
    private void Animation() {

        if (moveDir != Vector3.zero)
        {
            animator.SetBool("New Bool", true);
            
        }
        else { animator.SetBool("New Bool", false); }
        if (runing)
        {
            animator.SetBool("New Bool 0", true);
        }
        else
        {
            animator.SetBool("New Bool 0", false);
        }

    }
   

    private void ShakeInput()
    {
        SwitchToEventInput();
        
        puzzleList =new int[] { 3,4,3,4};
        phase = 0;
        for (int i = 0; i < puzzleList.Length; i++)
        {
            Debug.Log(puzzleList[i]);
        }
    }
    private void RotateInput()
    {
        SwitchToEventInput();
        puzzleList = new int[] { 2, 3, 1, 4 };
        phase = 0;
        for (int i = 0; i < puzzleList.Length; i++)
        {
            Debug.Log(puzzleList[i]);
        }
    }
    private void PushInput() {
        SwitchToPushInput();
      
    }
    /*
    private void RandomInput()
    {
        SwitchToEventInput();
        //difficulty 
        difficulty = target.GetComponent<MoraEvents>().GetInteractType();
        //make the puzzle
        puzzleList = new int[difficulty];
        for (int i = 0; i < difficulty; i++)
        {
            puzzleList[i] = UnityEngine.Random.Range(1, 6);
            Debug.Log(puzzleList[i]);
        }
        phase = 0;
    }
    */
    private void SwitchToEventInput() { 
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

}
