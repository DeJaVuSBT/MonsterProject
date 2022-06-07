using UnityEngine;
using System.Collections.Generic;

public class PlayerShakeState : PlayerBaseState
{
    
    private int currentInput =0;
    private int preInput = 0;
    private List<int> AllInput = new List<int>();
    private int counter = 0;
    //private int[] puzzleList=null;
    private bool puzzling = true;
    public PlayerShakeState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    GameObject tutObj;

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
        _manager.PuzzleList = null;
        _manager.SwitchToEventInput();
        //A you are pressing and B you are also pressing
        //joystick left, pull back = cancel, go right
        _manager.InPut.EventInput.Button5.started += Button5_started => currentInput=5;
        _manager.InPut.EventInput.Button4.started += Button4_started => currentInput=4;
        _manager.InPut.EventInput.Button3.started += Button3_started => currentInput=3;
        _manager.InPut.EventInput.Button2.started += Button2_started => currentInput=2;
        _manager.InPut.EventInput.Button1.started += Button1_started => currentInput=1;
        _manager.startTutorial(0);
        
    }

    public override void ExitState()
    {
        _manager.SwitchToPlayerInput();
        _manager.PuzzleList = null;
        _manager.endTutorial(0);
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
