using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera3d : MonoBehaviour
{
    public float rotateTime = 0.2f;
    private bool isRotating = false;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offSet= Vector2.zero;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = player.position+ offSet;

       // Rotate();
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateAround(-45, rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateAround(45, rotateTime));
        }
    }

    IEnumerator RotateAround(float angel, float time)
    {
        float number = 60 * time;
        float nextAngel = angel / number;
        isRotating = true;

        for (int i = 0; i < number; i++)
        {
            transform.Rotate(new Vector3(0, 0, nextAngel));
            yield return new WaitForFixedUpdate();
        }

        isRotating = false;
    }
}
