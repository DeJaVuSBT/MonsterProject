using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIconAnimation : MonoBehaviour
{
    private Camera mainCamera;
    private Transform mainCanvas;
    private Transform hungerBar;
    private Transform player;
    private Vector3 target;
    private Vector3 mcPosScreen;
    private Vector3 hungerPos;
    private bool moveUp;
    private bool landed;
    private bool sizeDown;
    private int sizeUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCanvas = GameObject.Find("Canvas").transform;
        hungerBar = mainCanvas.Find("HungerBar");
        hungerPos = hungerBar.Find("HungerIcon").position;
        target = new Vector3(0, 300, 0);

        player = GameObject.FindWithTag("Player").transform;
        mcPosScreen = mainCamera.WorldToScreenPoint(player.position);

        transform.position = mcPosScreen;
        transform.SetParent(mainCanvas.transform);
        Debug.Log(mcPosScreen);
    }

    // Update is called once per frame
    void Update()
    {
        mcPosScreen = mainCamera.WorldToScreenPoint(player.position);
        if (!moveUp)
        {
            float speed = 1000 * (0.01f + 2 * (0.5f - Mathf.Abs(0.5f - (transform.position.y - mcPosScreen.y) / target.y)));
            transform.position = Vector3.MoveTowards(transform.position, mcPosScreen + target, speed * Time.deltaTime);
            if (transform.position.y >= mcPosScreen.y + target.y)
            {
                moveUp = true;
            }
        }
        else
        {
            float speed = 1000 * (0.01f + 2 * (0.5f - Mathf.Abs(0.5f - (transform.position.y - hungerPos.y) / (target.y + mcPosScreen.y - hungerPos.y))));
            transform.position = Vector3.MoveTowards(transform.position, hungerPos, speed * Time.deltaTime);
            if (transform.position.y <= hungerPos.y)
            {
                this.GetComponent<Image>().enabled = false;
                landed = true;
            }
        }
        if (landed)
        {
            if (!sizeDown)
            {
                if (sizeUp < 50)
                {
                    hungerBar.localScale += new Vector3(0.005f, 0.005f, 0.005f);
                    sizeUp++;
                }
                else
                {
                    sizeDown = true;
                }
            }
            else
            {
                if (hungerBar.localScale.x > 1.0f)
                {
                    hungerBar.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                } else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
