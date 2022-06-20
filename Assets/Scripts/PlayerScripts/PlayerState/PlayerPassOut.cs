using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassOut : PlayerBaseState
{
    public PlayerPassOut(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
  
    }

    public override void EnterState()
    {
        _manager.Animator.SetBool("isDying", true);
        _manager.RB.isKinematic = true;
        //_manager.CageObj.SetActive(true);
        _manager.CageObj.GetComponent<Animator>().SetBool("destroyCage" , false);
        CutScene();
    }


    private void CutScene() {
        CinematicBars.Show_Static(4000, 2f);
        TimerAction.Create(() => _manager.Animator.SetBool("isDying", false), 2f);
        TimerAction.Create(() => _manager.CageObj.GetComponent<Animator>().SetBool("Open", false), 2f);
        TimerAction.Create(() => _manager.transform.position = _manager.CagePos.position, 2f);
        TimerAction.Create(() => CinematicBars.Hide_Static(2f), 2f);
        TimerAction.Create(() => SwitchState(_states.MoveState()), 2f);
        TimerAction.Create(() => _manager.OutOfCage = true , 2f);
    }
    public override void ExitState()
    {
        _manager.RB.isKinematic = false;
    }

    public override void UpdateState()
    {

    }

}
