using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerStateManager _manager;
    protected PlayerState _states;
    public PlayerBaseState(PlayerStateManager manager,PlayerState states) {
        _manager = manager;
        _states = states;
    }
    public abstract  void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract void CheckIfSwitchState();

   protected void SwitchState(PlayerBaseState newState) {
        ExitState();

        newState.EnterState();
        
        _manager.CurrentState = newState;
    }
}
