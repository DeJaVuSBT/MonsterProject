using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pressbuttonscript : MonoBehaviour
{
    float opacity;
    bool opacityDown;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (opacityDown)
        {
            if (opacity > 0)
            {
                opacity -= 0.02f;
            } else if (opacity <= 0)
            {
                opacityDown = false;
            }
        }
        if (!opacityDown)
        {
            if (opacity < 1)
            {
                opacity += 0.02f;
            } else if (opacity >= 1)
            {
                opacityDown = true;
            }
        }
        GetComponentInChildren<Text>().color = new Color(255, 255, 255, opacity);
        GetComponentInChildren<Image>().color = new Color(255, 255, 255, opacity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
