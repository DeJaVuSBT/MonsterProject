using System;
using System.Collections.Generic;
using UnityEngine;
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
    private float moveX, moveZ;
    private float oldX, oldZ;
    private bool beingCatched=false;
    [Header("Interaction")]
    [SerializeField]
    private GameObject target = null;
    private void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (!spriteRenderer)
        {

            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (!animator)
        {

            animator = GetComponent<Animator>();
        }

    }

    private void Update()
    {
        if (beingCatched)
        {
            rb.velocity = new Vector3(0,0,0);
        }
        else
        {
            InputCheck();
            Movement();
         //   Animation();
            Interact();
        }
    }
    #region Interaction


    //error!
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                target.GetComponent<Interactable>().Interact();
                CinematicBars.Show_Static(400, 0.3f);
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
    private void InputCheck()
    {
        moveX = 0f;
        moveZ = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveZ = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

       
    }
    private void Movement()
    {
        moveDir = new Vector3 (moveX,0,moveZ).normalized;
        rb.velocity= moveDir * moveSpeed;
    }
    public void BeingCatched(bool beCatched) {
        beingCatched = beCatched;
    }
}
