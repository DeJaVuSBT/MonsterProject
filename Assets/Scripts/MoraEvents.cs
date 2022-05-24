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
    [SerializeField]
    private int morality;
    [SerializeField]
    private int hunger;
    private bool shake = false;
    private float shaketime = 0;

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
    public void SetInteractType(int a) {
        interactType = (InteractType)a;
    }
    public void SetReward(int a, int b) { 
        morality= (int)a;
        hunger= (int)b;
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
    private void ShakingVisual() {
      
        this.gameObject.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f)); 
    }
    public void Shake() {
        shake = true;
        shaketime = 0;
    }
    private void Update()
    {

        if (shake)
        {
            ShakingVisual();
            shaketime+=Time.deltaTime;
            if (shaketime>0.2f)
            {
                shake = false;
            }
        }
       
    }

}
