using UnityEngine;
using UnityEngine.UI;
public class HungerBar : MonoBehaviour
{
    public static HungerBar Instance;
    public Image slider;
    
    [SerializeField]
    private int speed=10;
    void Start()
    {
        //slider = this.GetComponent<Image>();
        //slider.fillAmount = 100f;
    }

    //100=1 so amount/100
    public void Add(float amount) { 
        slider.fillAmount += amount / 100;
    }
    public void Decrease(float amount)
    {
        slider.fillAmount -= amount / 100;
    }
    public int GetMoralAmount() { 
        return (int)(slider.fillAmount*100);
    }
    private void Update()
    {
        Decrease(speed * Time.deltaTime);
    }
}
