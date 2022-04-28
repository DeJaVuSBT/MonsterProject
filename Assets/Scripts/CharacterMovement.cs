using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
        [SerializeField]
        private float moveSpeed = 4f;
        private Vector3 moveDir;
        [Header("Parts")]
        [SerializeField]
        private Animator animator;
        private Rigidbody2D rb;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        private float moveX, moveY;
        private float oldX,oldY;
        private Vector3 _movement;
    private void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
           
        if (!spriteRenderer)
        {

            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        /*
        if (!animator)
        {

            animator = GetComponent<Animator>();
        }
        */
    }

    private void Update()
    {
        InputCheck();
        Movement();
        Animation();
    }
    private void FixedUpdate()
    {
        
    }
    private void Animation() {

        if (moveDir != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            oldX = moveX;
            oldY = moveY;
        }
        else { animator.SetBool("IsMoving", false); }
        animator.SetFloat("InputX", oldX);
        animator.SetFloat("InputY", oldY);
        Debug.Log(oldX);
    }
    private void InputCheck()
    {
        moveX = 0f;
        moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
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
        moveDir = (moveX * transform.right + transform.up * moveY).normalized;
        rb.velocity = moveDir * moveSpeed;
    }
}
