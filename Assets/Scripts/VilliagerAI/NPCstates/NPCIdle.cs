using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdle : NPCStateBase
{
    public NPCIdle(NPCManager manager, NPCStates states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
        if (CheckIfPlayerClose()&&CheckIfMoralLow())
        {
            TimerAction.StopTimer("MHRoaming");
            SwitchState(_states.Chase());
        }
    }

    public override void EnterState()
    {
        Debug.Log("AI Idle");
        //animation
        TimerAction.Create(() => SwitchState(_states.Roaming()), 5f,"MHRoaming");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}
