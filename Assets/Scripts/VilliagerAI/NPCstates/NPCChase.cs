using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChase : NPCStateBase
{

    public NPCChase(NPCManager manager, NPCStates states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
        //chased up
        if (Vector3.Distance(_manager.transform.position, _manager.Player.transform.position) < 2f)
        {
            SwitchState(_states.Throw());
        }
    }
    public override void EnterState()
    {
        Debug.Log("AI Chasing");
        _manager.ShowEmotion(7);
        //spot player animation
        _manager.Animator.SetBool("IsSpotting", true);
        //wait animationdone
        TimerAction.Create(() => _manager.Animator.SetBool("IsSpotting", false), 1.05f, "StopSpot");
        TimerAction.Create(() => _manager.Animator.SetBool("IsRunning", true), 1.05f, "Run");
        TimerAction.Create(() => _manager.Agent.isStopped=false,1.05f);
    }

    public override void ExitState()
    {
        _manager.Animator.SetBool("IsRunning", false);
        _manager.Agent.isStopped = true;
        
    }
    public override void UpdateState()
    {
        if (_manager.Animator.GetBool("IsRunning"))
        {
            _manager.Agent.SetDestination(_manager.Player.transform.position);
            CheckIfSwitchState();
        }
        
    }

  
}
