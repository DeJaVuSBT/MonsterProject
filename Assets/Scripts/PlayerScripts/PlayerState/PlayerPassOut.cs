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
        CutScene();
    }


    private void CutScene() {
        CinematicBars.Show_Static(4000, 2f);
        //clean moral bar
        TimerAction.Create(() => _manager.Animator.SetBool("isDying", false), 2f);
        TimerAction.Create(() => _manager.transform.position = _manager.CagePos.position, 2f);
        TimerAction.Create(() => CinematicBars.Hide_Static(2f), 2f);
        TimerAction.Create(() => SwitchState(_states.MoveState()), 3f);
    }
    public override void ExitState()
    {
        _manager.RB.isKinematic = false;
    }

    public override void UpdateState()
    {

    }

}
