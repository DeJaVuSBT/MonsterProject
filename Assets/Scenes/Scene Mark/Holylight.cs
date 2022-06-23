using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holylight : MonoBehaviour
{
    float opacity;
    bool opacityDown;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (opacityDown)
        {
            if (opacity > 0.25)
            {
                opacity -= 0.005f;
            } else if (opacity <= 0.25)
            {
                opacityDown = false;
            }
        }
        if (!opacityDown)
        {
            if (opacity < 0.75)
            {
                opacity += 0.005f;
            } else if (opacity >= 0.75)
            {
                opacityDown = true;
            }
        }
        GetComponentInChildren<Image>().color = new Color(GetComponentInChildren<Image>().color.r, GetComponentInChildren<Image>().color.g, GetComponentInChildren<Image>().color.b, opacity);

       
    }
}
