using UnityEngine;

public abstract class NPCStateBase
{
    protected NPCManager _manager;
    protected NPCStates _states;
    public NPCStateBase(NPCManager manager, NPCStates states)
    {
        _manager = manager;
        _states = states;
    }

    public abstract void EnterState();

    public abstract void ExitState();

    //void update()
    public abstract void UpdateState();

    public abstract void CheckIfSwitchState();

    protected void CheckIfPlayerClose() { 
        
        
    }
   

    protected void SwitchState(NPCStateBase newState)
    {
        ExitState();

        newState.EnterState();

        _manager.CurrentState = newState;
    }
}
