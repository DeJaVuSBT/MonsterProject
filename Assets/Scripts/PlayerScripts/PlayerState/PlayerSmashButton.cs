using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSmashButton : PlayerBaseState
{
    private int counter = 4;
    private bool puzzling = true;
    private float total = 50;
    public PlayerSmashButton(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
        if (!puzzling)
        {
            SwitchState(_states.MoveState());
        }
    }

    public override void EnterState()
    {
        Debug.Log("SmashButton");
        _manager.Target.GetComponent<MoraEvents>().sBar.SetActive(true);
        _manager.SwitchToEventInput();
        _manager.InPut.EventInput.Button5.started += Button5_started => total+=counter;
        _manager.InPut.EventInput.Button4.started += Button4_started => puzzling = false;
        _manager.InPut.EventInput.Button3.started += Button3_started => puzzling = false;
        _manager.InPut.EventInput.Button2.started += Button2_started => puzzling = false;
        _manager.InPut.EventInput.Button1.started += Button1_started => puzzling = false;
        _manager.InPut.EventInput.AllKey.started += AllKey_started => _manager.Target.GetComponent<MoraEvents>().Shake();
        _manager.startTutorial(3);
    }

    public override void ExitState()
    {
        _manager.Target.GetComponent<MoraEvents>().sBar.SetActive(false);
        _manager.SwitchToPlayerInput();
        _manager.endTutorial(3);
    }

    public override void UpdateState()
    {
        CheckIfInputEnough();
        CheckIfSwitchState();
    }
    private void CheckIfInputEnough()
    {
        Debug.Log(total);
        total -= Time.deltaTime*10;
        _manager.Target.GetComponent<MoraEvents>().sBar.GetComponent<Slider>().value = total / 100;
        if (total<10)
        {
            puzzling = false;
        }
        else if(total>=99)
        {
            _manager.Target.GetComponent<Reward>().Reward();
            Debug.Log("Passed");
            puzzling = false;
        }
    }

}

