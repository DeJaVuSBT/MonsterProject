using System;
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
        _manager.Animator.SetBool("isHitting", true);
        _manager.Target.GetComponent<MoraEvents>().sBar.SetActive(true);
        _manager.SwitchToEventInput();
        _manager.InPut.EventInput.Button5.started += Button5_started;
        _manager.InPut.EventInput.Button4.started += Button4_started => puzzling = false;
        _manager.InPut.EventInput.Button3.started += Button3_started => puzzling = false;
        _manager.InPut.EventInput.Button2.started += Button2_started => puzzling = false;
        _manager.InPut.EventInput.Button1.started += Button1_started => puzzling = false;
        _manager.showTutorial("smashOn" , true);

    }

    private void Button5_started(InputAction.CallbackContext obj)
    {
        _manager.Target.GetComponent<MoraEvents>().SoundWhenHit();
        Debug.Log("SPacebar");
        _manager.Target.GetComponent<MoraEvents>().Shake();
        total += counter * 5;
        
    }

    public override void ExitState()
    {
        _manager.InPut.EventInput.Button5.started -= Button5_started;
        _manager.Animator.SetBool("isHitting", false);
        _manager.Target.GetComponent<MoraEvents>().sBar.SetActive(false);
        _manager.showTutorial("smashOn" , false);
    }

    public override void UpdateState()
    {
        CheckIfInputEnough();
        CheckIfSwitchState();
    }
    private void CheckIfInputEnough()
    {
        total -= Time.deltaTime*20;
        if (puzzling ==true)
        {
            _manager.Target.GetComponent<MoraEvents>().sBar.GetComponent<Slider>().value = total / 100;
        }
        
        if (total<10)
        {
            puzzling = false;
        }
        else if(total>=80)
        {
            _manager.Target.GetComponent<Reward>().Reward();
            puzzling = false;
        }

       

    }

}

