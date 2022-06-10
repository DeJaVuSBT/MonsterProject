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
    private bool destroyAtTheEnd = true;
    private bool rewarded = false;
    public MoralityBar mBar;
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
    private int morality;
    [SerializeField]
    private int hunger;
    private bool shake = false;
    private float shaketime = 0;
    [SerializeField]
    private GameObject newMbar;
    //getsett
    public int Selected { set { selected = value; } }
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
            return (int)interactType;
        }
        else if (doubleInteraction && selected == 2)
        {
            return (int)interactType1;
        }
        else
        {
            return (int)interactType;
        }

    }

    void Awake()
    {
        mBar = GameObject.FindGameObjectWithTag("MorBar").GetComponent<MoralityBar>();
        hBar = GameObject.FindGameObjectWithTag("HunBar").GetComponent<HungerBar>();
        sBar = GameObject.FindGameObjectWithTag("SmashBar");
        newMbar = GameObject.FindGameObjectWithTag("Mbar");
        option = GameObject.FindGameObjectWithTag("Option");

    }
    void Start()
    {
        sBar.SetActive(false);
        option.SetActive(false);
    }

    public void Reward()
    {
        if (!rewarded)
        {

            mBar.Add(morality);
            hBar.Add(hunger);
            if (GoodDeedorBadDeed)
            {
                newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("getBlue", true);
                TimerAction.Create(() => newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("getBlue", false), 3f);
                TimerAction.Create(() => newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("removeCard", true), 3f);
                TimerAction.Create(() => newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("removeCard", false), 3.5f);
            }
            else
            {
                newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("getRed", true);
                TimerAction.Create(() => newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("getRed", false), 3f);
                TimerAction.Create(() => newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("removeCard", true), 3f);
                TimerAction.Create(() => newMbar.transform.GetChild(0).GetComponent<Animator>().SetBool("removeCard", false), 3.5f);
            }


            if (destroyAtTheEnd)
            {
                Destroy(this.gameObject);
            }

            rewarded = true;
        }
        isInteracting = false;
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
    }
    public void HideOption()
    {
        option.SetActive(false);
    }
    public void SelectedAnimation() {
        selectedAnimationDone = false;
        TimerAction.Create(() => SelectedAniationOn(selected), 0.3f);
        TimerAction.Create(() => SelectedAniationoff(selected), 0.6f);
        TimerAction.Create(() => SelectedAniationOn(selected), 0.9f);
        TimerAction.Create(() => SelectedAniationoff(selected), 1.2f);
        TimerAction.Create(() => selectedAnimationDone=true, 1.2f);

    }
    private void SelectedAniationOn(int a) {
        option.transform.GetChild(a-1).gameObject.GetComponent<Outline>().effectDistance = new Vector2(10, 10);
    }
    private void SelectedAniationoff(int a)
    {
        option.transform.GetChild(a-1).gameObject.GetComponent<Outline>().effectDistance = new Vector2(1, 1);
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
}
