using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerStateManager _manager;
    protected PlayerState _states;
    public PlayerBaseState(PlayerStateManager manager,PlayerState states) {
        _manager = manager;
        _states = states;
    }
    //trigger once when enter   tutorial??visual showup
    public abstract void EnterState() ;
    //trigger once when exit     disable turorial? canvas?
    public abstract void ExitState();

    //void update()
    public abstract void UpdateState();

    //normal void  i will write down the condition when player switch state
    public abstract void CheckIfSwitchState();

   protected void SwitchState(PlayerBaseState newState) {
        ExitState();

        newState.EnterState();
        
        _manager.CurrentState = newState;
    }
}
