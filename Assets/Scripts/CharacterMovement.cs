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
        BasicMoveMentCheck();
        AnimationCheck();
    }
    private void FixedUpdate()
    {
        
    }
    private void AnimationCheck() {
        
    }
    private void BasicMoveMentCheck()
    {
        float moveX = 0f;
        float moveY = 0f;
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
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
            spriteRenderer.flipX = false;
        }

        moveDir = (moveX*transform.right+ transform.up*moveY).normalized;
       // moveDir = new Vector3 (moveDir.x, moveDir.y, 0);
        rb.velocity = moveDir * moveSpeed;
    }
}
