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
            SwitchState(_states.Chase());
        }
    }

    public override void EnterState()
    {
        //animation
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}
