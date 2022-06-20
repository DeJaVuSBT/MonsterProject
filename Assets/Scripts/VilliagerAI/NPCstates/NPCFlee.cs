using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFlee : NPCStateBase
{
    public NPCFlee(NPCManager manager, NPCStates states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
        if (!CheckIfPlayerClose())
        {
            SwitchState(_states.Idle());
        }
    }
    public override void EnterState()
    {
        _manager.Agent.isStopped = false;
        _manager.Agent.SetDestination(GetNextPos());
        _manager.Animator.SetBool("IsRunning", true);
    }

    public override void ExitState()
    {

        _manager.Agent.isStopped = true;
        _manager.Animator.SetBool("IsRunning", false);
    }
    public override void UpdateState()
    {
        CheckIfSwitchState();

    }
    Vector3 GetNextPos() {
        Vector3 diff = _manager.transform.position - _manager.Player.transform.position;
        return -diff.normalized*6;
    }

}
