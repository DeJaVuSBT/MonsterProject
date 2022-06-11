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

    protected bool CheckIfPlayerClose() {
      return  Vector3.Distance(_manager.transform.position, _manager.Player.transform.position) < _manager.SeneseRange?  true: false;
    }

    protected int GetMoral() {
        return _manager.MBar.GoodEOrBadE;
    }
    protected bool CheckIfMoralLow() {

        if (GetMoral()==1)
        {
            return true;
        }
        else if (GetMoral()==0)
        {
            return false;
        }
        else
        {
            return false;
        }
           
    }
    protected bool CheckIfMoralHigh() {
        if (GetMoral() == 1)
        {
            return false;
        }
        else if (GetMoral() == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    protected void SwitchState(NPCStateBase newState)
    {
        ExitState();

        newState.EnterState();

        _manager.CurrentState = newState;
    }
}
