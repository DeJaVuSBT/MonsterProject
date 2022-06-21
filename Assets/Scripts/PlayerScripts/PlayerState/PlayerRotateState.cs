using UnityEngine;
using System.Collections.Generic;
public class PlayerRotateState : PlayerBaseState
{
    public PlayerRotateState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    private int currentInput = 0;
    private int preInput = 0;
    private int counter = 0;
    private List<int> AllInput = new List<int>();
    private bool puzzling = true;
    public override void EnterState()
    {
        Debug.Log("Shake");
        _manager.Animator.SetBool("isShaking", true);
        _manager.PuzzleList = null;
        _manager.SwitchToEventInput();
        _manager.InPut.EventInput.Button5.performed += Button5_started => currentInput = 5;
        _manager.InPut.EventInput.Button4.performed += Button4_started => currentInput = 4;
        _manager.InPut.EventInput.Button3.performed += Button3_started => currentInput = 3;
        _manager.InPut.EventInput.Button2.performed += Button2_started => currentInput = 2;
        _manager.InPut.EventInput.Button1.performed += Button1_started => currentInput = 1;

        _manager.showTutorial("rotateOn" , true);
    }
    public override void CheckIfSwitchState()
    {
        if (!puzzling)
        {
            SwitchState(_states.MoveState());
        }
    }


    public override void ExitState()
    {
        _manager.Animator.SetBool("isShaking", false);
        _manager.SwitchToPlayerInput();
        _manager.PuzzleList = null;

        _manager.showTutorial("rotateOn" , false);
    }

    public override void UpdateState()
    {
        CheckIfSwitchState();
        CollectInput();
        Check();
    }
    private void CollectInput()
    {
        if (preInput != currentInput)
        {
            AllInput.Add(currentInput);
            preInput = currentInput;
            counter++;
            _manager.Target.GetComponent<MoraEvents>().SoundWhenShake();
            _manager.Target.GetComponent<MoraEvents>().Shake();
            if (counter == 1)
            {
                CheckTheFirstInput();
            }
        }
    }
    private bool CheckIfInputCorrect()
    {
        if (_manager.PuzzleList[counter - 1] == AllInput[counter - 1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Check()
    {
        if (counter >= 1 & puzzling)
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

    private void CheckTheFirstInput()
    {
        switch (currentInput)
        {
            case 1:
                _manager.PuzzleList = new int[] { 1, 4, 2, 3 };
                break;
            case 2:
                _manager.PuzzleList = new int[] { 2, 3, 1, 4 };
                break;
            case 3:
                _manager.PuzzleList = new int[] { 3, 1, 4, 2 };
                break;
            case 4:
                _manager.PuzzleList = new int[] { 4, 2, 3, 1 };
                break;
            case 5:
                puzzling = false;
                break;
        }
    }
}