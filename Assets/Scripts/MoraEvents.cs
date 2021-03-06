using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoraEvents : MonoBehaviour, Interactable, Reward
{


    [SerializeField]
    public bool GoodDeedorBadDeed = true;
    public bool doubleInteraction = false;
    private int selected=0;
    public bool selectedAnimationDone = false;
    public bool destroyAtTheEnd = true;
    private bool rewarded = false;
    public HungerBar hBar;
    public GameObject sBar;
    [SerializeField]
    private GameObject option;
    private bool isInteracting = false;
    [SerializeField]
    private InteractType interactType;
    [SerializeField]
    private InteractType2 interactType1;
    [SerializeField]
    private int hunger;
    private bool shake = false;
    private float shaketime = 0;
    [SerializeField]
    private GameObject newMbar;
    //getsett
    public int Selected { set { selected = value; } }
    public int GetHunger { get { return hunger; } }
    //option Highlight
    GameObject badHighlight , goodHighlight;

    PlayerStateManager _manager;
    enum InteractType
    {
        Shaking,
        Rotating,
        Take,
        Smash
    }
    enum InteractType2
    {
        Shaking,
        Rotating,
        Smash
    }
    public int GetInteractType()
    {
        Debug.Log((int)interactType);
        if (doubleInteraction && selected == 1)
        {
            GoodDeedorBadDeed = false;
            return (int)interactType;
        }
        else if (doubleInteraction && selected == 2)
        {
            GoodDeedorBadDeed = true;
            return (int)interactType1;
        }
        else
        {
            return (int)interactType;
        }

    }

    void Awake()
    {
        hBar = GameObject.FindGameObjectWithTag("HunBar").GetComponent<HungerBar>();
        sBar = GameObject.FindGameObjectWithTag("SmashBar");
        newMbar = GameObject.FindGameObjectWithTag("Mbar");
        option = GameObject.FindGameObjectWithTag("Option");
        badHighlight = GameObject.Find("badHighlight");
        goodHighlight = GameObject.Find("goodHighlight");
        //_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>();

    }
    void Start()
    {
        sBar.SetActive(false);
        option.SetActive(false);

        badHighlight.SetActive(false);
        goodHighlight.SetActive(false);
    }

    public void Reward()
    {
        if (!rewarded)
        {
            if (GoodDeedorBadDeed)
            {
                newMbar.GetComponent<DeedSwitch>().AddCard(GoodDeedorBadDeed);
                hBar.Add(hunger);
                destroyAtTheEnd = false;
            }
            else
            {
                newMbar.GetComponent<DeedSwitch>().AddCard(GoodDeedorBadDeed);
                hBar.Add(hunger);   
            }

            if (destroyAtTheEnd)
            {
                Destroy(this.gameObject);
            }

            if (this.gameObject.tag=="Cage")
            {
                this.GetComponent<Animator>().SetBool("Open", true);
            }
            else
            {
                Destroy(this);
            }


            rewarded = true;
        }
        isInteracting = false;

        //TimerAction.Create(() => inputOn(), 2.0f);
    }

    public void Interact()
    {
        isInteracting = true;
        selectedAnimationDone = false;
    }
    public void Shake()
    {
        shake = true;
        shaketime = 0;
    }

    public void ShowOption()
    {
        option.SetActive(true);
        
        setOptionsAnimations(true);
        
    }
    public void HideOption()
    {
        option.SetActive(false);

        setOptionsAnimations(false);
    }


    //call this function to check the interact type and set the animator booleans for the bad/good options
    public void setOptionsAnimations(bool _value)
    {

        Animator optionBad = option.transform.Find("badOption").gameObject.GetComponent<Animator>();
        Animator optionGood = option.transform.Find("goodOption").gameObject.GetComponent<Animator>();

        if((int)interactType == 0){optionBad.SetBool("shakeOn" , _value);}
        if((int)interactType == 1){optionBad.SetBool("rotateOn" , _value);}
        if((int)interactType == 3){optionBad.SetBool("smashOn" , _value);}

        if((int)interactType1 == 0){optionGood.SetBool("shakeOn" , _value);}
        if((int)interactType1 == 1){optionGood.SetBool("rotateOn" , _value);}
        if((int)interactType1 == 3){optionGood.SetBool("smashOn" , _value);}
    }

    public void SelectedAnimation() {
        selectedAnimationDone = false;
        TimerAction.Create(() => SelectedAnimationOn(selected), 0.3f);
        TimerAction.Create(() => SelectedAnimationoff(), 0.6f);
        TimerAction.Create(() => SelectedAnimationOn(selected), 0.9f);
        TimerAction.Create(() => SelectedAnimationoff(), 1.2f);
        TimerAction.Create(() => selectedAnimationDone=true, 1.2f);

    }
    private void SelectedAnimationOn(int a) {
        //option.transform.GetChild(a-1).gameObject.GetComponent<Outline>().effectDistance = new Vector2(10, 10);
        if(a == 1){badHighlight.SetActive(true);}
        if(a == 2){goodHighlight.SetActive(true);}
    }
    private void SelectedAnimationoff()
    {
        //.transform.GetChild(a-1).gameObject.GetComponent<Outline>().effectDistance = new Vector2(1, 1);
        badHighlight.SetActive(false);
        goodHighlight.SetActive(false);
    }

    private void Update()
    {

        if (shake)
        {
            ShakingVisual();
            shaketime += Time.deltaTime;
            if (shaketime > 0.2f)
            {
                shake = false;
            }
        }

    }
    private void ShakingVisual()
    {
        this.gameObject.transform.localScale = new Vector3(UnityEngine.Random.Range(0.9f, 1.1f), UnityEngine.Random.Range(0.9f, 1.1f), UnityEngine.Random.Range(0.9f, 1.1f));
    }

    void inputOn()
    {
        _manager.SwitchToPlayerInput();      
    }
    void inputOff()
    {
        _manager.SwitchToWaitState();
    }
}
