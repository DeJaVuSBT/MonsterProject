using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeedSwitch : MonoBehaviour
{
    public int timer = 10;
    [SerializeField]
    private GameObject badP, generalP, goodP, bigCard;
    [SerializeField]
    private GameObject cardPrefab;
    private Queue<GameObject> cardList = new Queue<GameObject>();
    private Queue<bool> cardgbList = new Queue<bool>();
    private int bgCounter = 0;
    private int GoodOrBadEnding = 0;
    private float removingCounter = 0;
    private bool isRemoving = false;
    private PlayerStateManager pm;
    private HorizontalLayoutGroup layoutGroup;


    public int GoodEOrBadE { get { return GoodOrBadEnding; } }
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>();
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
        if (cardList.Count > 2)
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
                pm.Getfc.SetType = FormChanger.UnitType.NL;

            }
        }

    }

    private void SwitchPBad() {
        badP.SetActive(true);
        generalP.SetActive(false);
        GoodOrBadEnding = 1;
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
    }

    private void SwitchPNormal()
    {
        badP.SetActive(false);
        generalP.SetActive(true);
        GoodOrBadEnding = 0;
    }

    private void SwitchPGood()
    {
        goodP.SetActive(true);
        generalP.SetActive(false);
        GoodOrBadEnding = 2;
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
        TimerAction.Create(() => Destroy(a.gameObject), 1f);
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
