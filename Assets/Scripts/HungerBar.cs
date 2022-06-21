using UnityEngine;
using UnityEngine.UI;
public class HungerBar : MonoBehaviour
{
    public Image slider;
    GameObject hungerVFX;
    private PlayerStateManager playerManager;
    private bool resetH=false;
    private bool lower25 = false;
        private bool lower50 = false;
    
    [SerializeField]
    private float speed=1;
    SoundManager sm;

    private void Awake()
    {
        hungerVFX = GameObject.Find("HungerVFX");
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>();
    }
    void Start()
    {
        //slider = this.GetComponent<Image>();
        //slider.fillAmount = 100f;
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<SoundManager>();
        hungerVFX.SetActive(false);
    }

    //100=1 so amount/100
    public void Add(float amount) { 
        slider.fillAmount += amount / 100;
        sm.PlaySound(SoundManager.Sound.Eat);
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

        if(slider.fillAmount < 0.25f&&!lower25)
        {
            GetComponent<Animator>().SetBool("lowFood" , true);
            sm.PlaySound(SoundManager.Sound.Hunger25);
            hungerVFX.SetActive(true);
            lower25 = true;
        }
        else if (slider.fillAmount > 0.25f&& slider.fillAmount<0.5f&&!lower50)
        {
            sm.PlaySound(SoundManager.Sound.Hunger50);
            lower50 = true;
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
            sm.PlaySound(SoundManager.Sound.Hunger0);
            playerManager.BeingCaught();
            lower50 = false;
            lower25 = false;
            resetH = false;
        }
    }
}
