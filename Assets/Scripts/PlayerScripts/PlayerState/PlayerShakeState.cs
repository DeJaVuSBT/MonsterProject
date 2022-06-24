using UnityEngine;
using System.Collections.Generic;

public class PlayerShakeState : PlayerBaseState
{
    
    private int currentInput =0;
    private int preInput = 0;
    private List<int> AllInput = new List<int>();
    private int counter = 0;
    private bool puzzling = true;
    public PlayerShakeState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }

    public override void CheckIfSwitchState()
    {
        if (!puzzling)
        {
            SwitchState(_states.MoveState());
        }
    }

    public override void EnterState()
    {
        Debug.Log("Shake");
        _manager.Animator.SetBool("isShaking", true);
        _manager.PuzzleList = null;
        _manager.SwitchToEventInput();
        _manager.InPut.EventInput.Button5.performed += Button5_started => { currentInput = 5; Debug.Log("space"); };
        _manager.InPut.EventInput.Button4.performed += Button4_started => { currentInput = 4; Debug.Log("d");};
        _manager.InPut.EventInput.Button3.performed += Button3_started => { currentInput = 3; Debug.Log("a"); };
        _manager.InPut.EventInput.Button2.performed += Button2_started => { currentInput = 2; Debug.Log("s"); };
        _manager.InPut.EventInput.Button1.performed += Button1_started => { currentInput = 1; Debug.Log("w"); };
        _manager.showTutorial("shakeOn" , true);
        
    }

    public override void ExitState()
    {
        _manager.soundManager.StopSound();
        _manager.Animator.SetBool("isShaking", false);
        _manager.SwitchToPlayerInput();
        _manager.PuzzleList = null;
        _manager.showTutorial("shakeOn" , false);
    }

    public override void UpdateState()
    {
        CheckIfSwitchState();
        CollectInput();
        Check();
    }
    private void CollectInput() {
        if (preInput!=currentInput)
        {
            AllInput.Add(currentInput);
            preInput = currentInput;
            counter++;
            _manager.Target.GetComponent<MoraEvents>().Shake();
            if (counter==1)
            {
                CheckTheFirstInput();
                _manager.soundManager.PlaySound(SoundManager.Sound.TreeShake);
            }
        }
    }


    private void Check()
    {
        if (counter >= 1&puzzling)
        {
            if (CheckIfInputCorrect())
            {
                if (counter == _manager.PuzzleList.Length)
                {
                    _manager.Target.GetComponent<Reward>().Reward();
                    
                    //_manager.addCard(1);
                    puzzling = false;
                }
            }
            else
            {
                puzzling = false;
            }
        }
    }

    private bool CheckIfInputCorrect()
    {
        if (_manager.PuzzleList[counter - 1] == AllInput[counter-1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void CheckTheFirstInput() {
        switch (currentInput) {
            case 1:
                puzzling = false;
                break;
            case 2:
                puzzling = false;
                break;
            case 3:
                _manager.PuzzleList = new int[]{ 3, 4, 3, 4};
                break;
            case 4:
                _manager.PuzzleList = new int[] { 4, 3, 4, 3 };
                break;
            case 5:
                puzzling = false;
                break;
        }
    }

}
