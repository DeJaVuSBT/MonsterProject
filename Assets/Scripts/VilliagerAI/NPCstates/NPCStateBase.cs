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

    protected float GetMoral() {
        return _manager.MBar.slider.value * 100;
    }
    protected bool CheckIfMoralLow() {

        return GetMoral() < _manager.LowMoral ? true : false;
    }
    protected bool CheckIfMoralHigh() {
        return GetMoral() > _manager.HighMoral ? true : false;
    }
    protected void SwitchState(NPCStateBase newState)
    {
        ExitState();

        newState.EnterState();

        _manager.CurrentState = newState;
    }
}
