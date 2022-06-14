using UnityEngine;
using UnityEngine.UI;
public class HungerBar : MonoBehaviour
{
    public Image slider;
    GameObject hungerVFX;
    private PlayerStateManager playerManager;
    private bool resetH=false;
    
    [SerializeField]
    private float speed=1;

    private void Awake()
    {
        hungerVFX = GameObject.Find("HungerVFX");
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>();
    }
    void Start()
    {
        //slider = this.GetComponent<Image>();
        //slider.fillAmount = 100f;
        hungerVFX.SetActive(false);
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

        if(slider.fillAmount < 0.25f)
        {
            GetComponent<Animator>().SetBool("lowFood" , true);
            hungerVFX.SetActive(true);
        }
        else
        {
            GetComponent<Animator>().SetBool("lowFood" , false);
            hungerVFX.SetActive(false);
        }


        if (slider.fillAmount < 0.01f) { resetH = true; }
        if(resetH)
        {
            slider.fillAmount = 0.8f;
            playerManager.BeingCaught();
            resetH = false;
        }
    }
}
