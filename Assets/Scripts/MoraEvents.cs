using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraEvents : MonoBehaviour, Interactable,Reward
{
   
    
    [SerializeField]
    private bool destroyAtTheEnd = true;
    private bool rewarded = false;
    private MoralityBar mBar;
    private HungerBar hBar;
    private bool isInteracting = false;
    [SerializeField]
    private InteractType interactType;
    private int morality;
    private int hunger;

    enum InteractType 
    { 
        Shaking,
        Rotating,
        Take
    }
    public int GetInteractType() {
        Debug.Log((int)interactType);
        return (int)interactType;
    }

    void Start()
    {
        mBar = GameObject.Find("Canvas/MoralityBar").GetComponent<MoralityBar>();
        hBar = GameObject.Find("Canvas/HungerBar").GetComponent<HungerBar>();
    }
    
    public void Reward()
    {
        if (!rewarded) {

            mBar.Add(morality);
            hBar.Add(hunger);
            
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
    }
    public void ShakingVisual() {
        this.gameObject.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f)); ;
    }

   

}
