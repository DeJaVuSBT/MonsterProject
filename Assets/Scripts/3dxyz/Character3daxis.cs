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
    private float interactRange = 1f;
    private Vector3 moveDir;
    private float moveX, moveZ;
    private float oldX, oldZ;
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
        InputCheck();
        Movement();
        Animation();
        Interact();
    }
    #region Interaction
    private GameObject ClosedColliderAround(){
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] ColliderAround = Physics2D.OverlapCircleAll(playerPos, interactRange);

        //collider2d[] includes playerobj itself
        if (ColliderAround.Length > 1)
        {
            Collider2D Closest = null;
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
                if (Vector2.Distance(playerPos, new Vector2(Closest.transform.position.x, Closest.transform.position.y)) > Vector2.Distance(playerPos, new Vector2(ColliderAround[i].transform.position.x, ColliderAround[i].transform.position.y)))
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
            }

        }
    }
    #endregion
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
}
