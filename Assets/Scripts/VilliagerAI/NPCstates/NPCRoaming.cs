using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCRoaming : NPCStateBase
{
    public NPCRoaming(NPCManager manager, NPCStates states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
        if (CheckIfPlayerClose() && CheckIfMoralLow())
        {
            SwitchState(_states.Chase());
        }
        if (_manager.Agent.remainingDistance<0.1f||_manager.Agent.pathStatus== NavMeshPathStatus.PathPartial|| _manager.Agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            SwitchState(_states.Idle());
        }
    }

    public override void EnterState()
    {
        Debug.Log("AI Roaming");
        _manager.Agent.isStopped = false;
        _manager.Agent.SetDestination( GetNextPos());
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
        //Debug.Log(_manager.Agent.pathStatus);
    }
    Vector3 GetNextPos() {
        Vector3 nPos = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f,3f)) + _manager.OriginalPos;
        Debug.Log(nPos);
        return nPos;
    }
}
