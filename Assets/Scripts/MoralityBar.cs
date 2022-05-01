using UnityEngine;
using UnityEngine.UI;
public class MoralityBar : MonoBehaviour
{
    public static MoralityBar Instance;
    public Slider slider;
    void Start()
    {
    }

    //100=1 so amount/100
    public void Add(float amount) { 
        slider.value+= amount / 100;
    }
    public void Decrease(float amount)
    {
        slider.value -= amount / 100;
    }
}
