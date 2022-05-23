using UnityEngine;
using UnityEngine.UI;
public class HungerBar : MonoBehaviour
{
    public static HungerBar Instance;
    public Slider slider;
    [SerializeField]
    private int speed=10;
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
    public int GetMoralAmount() { 
        return (int)(slider.value*100);
    }
    private void Update()
    {
        Decrease(speed * Time.deltaTime);
    }
}
