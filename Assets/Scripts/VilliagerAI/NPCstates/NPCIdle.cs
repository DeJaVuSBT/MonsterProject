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
            if (_manager.gameObject.tag == "MH")
            {
                TimerAction.StopTimer("MHRoaming");
                SwitchState(_states.Chase());
            }
            else 
            {
                SwitchState(_states.Flee());
            }

        }
    }

    public override void EnterState()
    {
        Debug.Log("AI Idle");
        //animation
        TimerAction.Create(() => SwitchState(_states.Roaming()), 5f,"MHRoaming");
        TimerAction.Create(() => _manager.ShowEmotion(Random.Range(0,7)), Random.Range(0.5f,1.5f),"IdleEmoji");
     
    }

    public override void ExitState()
    {
        TimerAction.StopTimer("IdleEmoji");
    }

    public override void UpdateState()
    {
        CheckIfSwitchState();
    }
}
