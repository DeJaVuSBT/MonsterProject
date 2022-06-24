using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeedSwitch : MonoBehaviour
{
    public int timer = 10;
    [SerializeField]
    private GameObject badP, generalP, goodP, bigCard, goodEffect, neutralEffect;
    [SerializeField]
    private GameObject cardPrefab;
    private Queue<GameObject> cardList = new Queue<GameObject>();
    private Queue<bool> cardgbList = new Queue<bool>();
    private int GoodOrBadEnding = 0;
    private float removingCounter = 0;
    private bool isRemoving = false;
    private PlayerStateManager pm;
    private HorizontalLayoutGroup layoutGroup;
    SoundManager sm;
    private bool demon;
    public int GoodEOrBadE { get { return GoodOrBadEnding; } }
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>();
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<SoundManager>();
        layoutGroup = this.GetComponent<HorizontalLayoutGroup>();
        generalP.SetActive(true);
        bigCard.SetActive(false);
    }

    public void AddCard(bool gb) {

        AddCardAnimation(gb);
        TimerAction.Create(() => AddCardToMbar(gb), 1.3f);
    }

    private void AddCardToMbar(bool gb) {
        GameObject a = Instantiate(cardPrefab, this.transform);
        GetBlueOrRedCheck(a, gb);
        cardList.Enqueue(a);
        cardgbList.Enqueue(gb);
        Check();
    }

    private void AddCardAnimation(bool gb) {
        bigCard.SetActive(true);
        bigCard.GetComponent<Animator>().SetTrigger("GainTrigger");
        GameObject a = bigCard.transform.GetChild(0).gameObject;
        GetBlueOrRedCheck(a, gb);
        TimerAction.Create(() => bigCard.SetActive(false), 1.2f);
        //need 1f time finish it
    }

    private void GetBlueOrRedCheck(GameObject a, bool gb) {
        if (gb)
        {
            a.GetComponent<Animator>().SetTrigger("Blue");
        }
        else
        {

            a.GetComponent<Animator>().SetTrigger("Red");
        }
    }

    public void Check() {
        if (cardList.Count > 5)
        {
            RemoveACard();
        }
        if (cardList.Count > 0)
        {
            int badCard = 0;
            int goodCard = 0;
            foreach (bool bg in cardgbList)
            {
                if (bg)
                {
                    goodCard++;
                }
                else
                {
                    badCard++;
                }
            }
            if (goodCard == 5)
            {
                //good
                SwitchPGood();
                pm.Getfc.SetType = FormChanger.UnitType.G;
            }
            else if(badCard>=3)
            {
                //bad
                SwitchPBad();
                pm.Getfc.SetType = FormChanger.UnitType.E;
            }
            else
            {
                //neutral
                SwitchPNormal();
                if (!pm.GetOldDir)
                {
                        pm.Getfc.SetType = FormChanger.UnitType.NL;
                }
                else
                {
                    pm.Getfc.SetType = FormChanger.UnitType.NR; 
                }
               

            }
        }

    }

    private void SwitchPBad() {
        badP.SetActive(true);
        generalP.SetActive(false);
        GoodOrBadEnding = 1;
        if (!demon)
        {
            sm.PlaySound(SoundManager.Sound.Demon);
            demon = true;
        }
       
    }

    public void Reset()
    {
        SwitchPNormal();
        isRemoving = false;
        removingCounter = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        cardgbList.Clear();
        cardList.Clear();
        demon = false;
    }

    private void SwitchPNormal()
    {
        goodEffect.SetActive(false);
        neutralEffect.SetActive(true);
        badP.SetActive(false);
        goodP.SetActive(false);
        generalP.SetActive(true);
        GoodOrBadEnding = 0;
        demon = false;
    }

    private void SwitchPGood()
    {
        neutralEffect.SetActive(false);
        goodEffect.SetActive(true);
        goodP.SetActive(true);
        generalP.SetActive(false);
        GoodOrBadEnding = 2;
        sm.PlaySound(SoundManager.Sound.Angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfTimeRemove())
        {
            removingCounter = 0;
            isRemoving = true;
        }
        if (isRemoving)
        {
            removingCounter += Time.deltaTime;
            if (removingCounter > timer)
            {

                RemoveACard();
                Check();
                if (!CheckIfTimeRemove())
                {
                    isRemoving = false;
                }

                removingCounter = 0;
            }
        }


        //Changes the padding of the horlayout group so that cards appear from left and fit the ui
        layoutGroup.padding.right = 730 - 130*cardList.Count;
        foreach(RectTransform rect in this.GetComponent<RectTransform>())
        {
            LayoutRebuilder.MarkLayoutForRebuild(rect);
        }
    }

    private void RemoveACard() {
        Animator a = cardList.Dequeue().GetComponent<Animator>();
        a.SetBool("removeCard", true);
        TimerAction.Create(() => Destroy(a.gameObject), 1.25f);
        cardgbList.Dequeue();
    }

    private int getRightPadding()
    {
        /*
        if(cardList.Count == 1){return new Vector2(75 , 600);}
        if(cardList.Count == 2){return new Vector2(75 , 465);}
        if(cardList.Count == 3){return new Vector2(75 , 335);}
        if(cardList.Count == 4){return new Vector2(75 , 205);}
        if(cardList.Count == 5){return new Vector2(75 , 75);}
        */

        return 730 - 130*cardList.Count;
    }

    private bool CheckIfTimeRemove() {
        return cardgbList.Count != 0&&!isRemoving;
    }
}
