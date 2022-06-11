using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeedSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject badP, generalP, goodP,bigCard;
    [SerializeField]
    private GameObject cardPrefab;
    private Queue<GameObject> cardList = new Queue< GameObject>();
    private Queue<bool> cardgbList = new Queue<bool>();
    private int bgCounter=0;
    private bool isMaxed=false;
    void Start()
    {
        generalP.SetActive(true);
        bigCard.SetActive(false);
    }
    public void AddCard(bool gb) {
        AddCardAnimation( gb);
        TimerAction.Create(() => AddCardToMbar(gb), 1.3f);
    }

    private void AddCardToMbar(bool gb) {
        GameObject a = Instantiate(cardPrefab, transform);
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

    private void GetBlueOrRedCheck(GameObject a,bool gb) {
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
        if (cardList.Count>5)
        {
            Animator a = cardList.Dequeue().GetComponent<Animator>();
            a.SetBool("removeCard", true);
            TimerAction.Create(() => Destroy(a.gameObject), 1f);
            cardgbList.Dequeue();
        }
        else if (cardList.Count==5)
        {
            isMaxed = true;
            foreach (bool obj in cardgbList)
            {
                if (obj)
                {
                    bgCounter++;
                }
            }
        }

        if (isMaxed)
        {
            if (bgCounter>=3)
            {
                //good
                SwitchPGood();
            }
            else
            {
                //bad
                SwitchPBad();
            }
        }
    }

    private void SwitchPBad() {
        badP.SetActive(true);
        generalP.SetActive(false);
    }
    private void SwitchPGood()
    {
        goodP.SetActive(true);
        generalP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
