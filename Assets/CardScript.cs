using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CardScript : MonoBehaviour
{
    Animator cardController;
    //public bool flipFinished;
    public int cardStatus; //0=Default , 1=RedCard , 2=BlueCard

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        cardStatus = 0;
    }
    
    void Start()
    {
        cardController = GetComponent<Animator>();
        setRed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStatus(int _status)
    {
        if(_status == 1){setRed();}else{setBlue();}; //Call this funciton from other scripts to change the card 1=red 2=blue
    }

    void setRed() //Plays the animation and sets cardstatus to red card
    {
        cardController.SetBool("getRed" , true);
        cardStatus = 1;
        resetParameterWhenDone("CardFlipRed" , "getRed");
    }

    void setBlue() //Plays the animation and sets cardstatus blue card
    {
        cardController.SetBool("getBlue" , true);
        cardStatus = 2;
        resetParameterWhenDone("CardFlipBlue" , "getRed");
    }

    public void removeCard() //call this function from another script to remove the card
    {
        cardController.SetBool("removeCard" , true);
        cardStatus = 0;
        string stateToCheck = "";
        if(cardStatus == 1)
        {
            stateToCheck = "CardFlipRedToNorm";
        }
        if(cardStatus == 2)
        {
            stateToCheck = "CardFlipBlueToNorm";
        }

        resetParameterWhenDone(stateToCheck , "removeCard");
    }

    void resetParameterWhenDone(string _stateName , string _Parameter) //Reset parameter when animation has finished playing
    {
        if(!checkIfPlaying(_stateName))
        {
            cardController.SetBool(_Parameter , false);
        }
    }

   bool AnimatorIsPlaying(){
      return cardController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1; //checks if animation has finished
   }

     bool checkIfPlaying(string stateName){
     return AnimatorIsPlaying() && cardController.GetCurrentAnimatorStateInfo(0).IsName(stateName); //use this function to check if a specific animation has finished playing
  }
}
