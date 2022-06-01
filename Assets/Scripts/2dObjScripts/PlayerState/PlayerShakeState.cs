using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShakeState : PlayerBaseState
{
    private int currentInput=0;
    private int counter = 0;
    //private int[] puzzleList=null;
    private bool puzzling = true;
    private bool frstcheck = true;
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
        _manager.PuzzleList = null;
        frstcheck = true;
        _manager.SwitchToEventInput();
        _manager.InPut.EventInput.Button5.started += Button5_started => currentInput = 5;
        _manager.InPut.EventInput.Button4.started += Button4_started => currentInput = 4;
        _manager.InPut.EventInput.Button3.started += Button3_started => currentInput = 3;
        _manager.InPut.EventInput.Button2.started += Button2_started => currentInput = 2;
        _manager.InPut.EventInput.Button1.started += Button1_started => currentInput = 1;
        _manager.InPut.EventInput.AllKey.canceled += AllKey_canceled;
       _manager.InPut.EventInput.AllKey.started += AllKey_started =>_manager.Target.GetComponent<MoraEvents>().Shake();
    }

    public override void ExitState()
    {
     //   _manager.InPut.EventInput.Button5.started -= Button5_started;
     //   _manager.InPut.EventInput.Button4.started -= Button4_started;
     //   _manager.InPut.EventInput.Button3.started -= Button3_started;
     //   _manager.InPut.EventInput.Button2.started -= Button2_started;
     //   _manager.InPut.EventInput.Button1.started -= Button1_started;
        _manager.InPut.EventInput.AllKey.canceled -= AllKey_canceled;
     //   _manager.InPut.EventInput.AllKey.started  -=  AllKey_started;
        _manager.SwitchToPlayerInput();
        _manager.PuzzleList = null;
    }

    public override void UpdateState()
    {
        CheckIfSwitchState();
    }
    private bool CheckIfInputCorrect()
    {

        Debug.Log("PUzzle:"+_manager.PuzzleList[counter-1]+"  input:"+currentInput);
        if (_manager.PuzzleList[counter - 1] == currentInput)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void AllKey_canceled(InputAction.CallbackContext obj)
    {
        counter++;
        if (frstcheck)
        {
            CheckTheFirstInput();
            Debug.Log("check first input");
            frstcheck = false;
        }
        else
        {
            if (CheckIfInputCorrect())
            {
                
                Debug.Log("Good");
                if (counter == _manager.PuzzleList.Length)
                {
                    _manager.Target.GetComponent<Reward>().Reward();
                    Debug.Log("Passed");
                    puzzling = false;
                }
            }
            else
            {
                Debug.Log("failed");
                puzzling = false;
            }
        }
       
        Debug.Log("pressed times:"+counter);
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
