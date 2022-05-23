using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraEvents : MonoBehaviour, Interactable,Reward
{
    [SerializeField]
    private int morality;
    [SerializeField]
    private int hunger;
    [SerializeField]
    private bool rewarded = false;
    public MoralityBar mBar;
    public HungerBar hBar;
    [SerializeField]
    private bool isInteracting = false;
    [SerializeField]
    private InteractType interactType;
    
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
    }
    
    public void Reward()
    {
        if (!rewarded) {

            mBar.Add(morality);
            hBar.Add(hunger);
            rewarded = true;
        }
        isInteracting = false;
    }

    public void Interact()
    {
        isInteracting = true;
    }
    private void ShakingVisual() {
        this.gameObject.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f)); ;
    }

    void Update() {
        if ((int)interactType== 0&&isInteracting)
        {
            ShakingVisual();
        }
    }

}
