using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
        if (!_manager.Running)
        {
            SwitchState(_states.MoveState());
        }
    }

    public override void EnterState()
    {
        Debug.Log("Run");
        _manager.Running = true;
        _manager.ApplyRunSpeed();
        _manager.InPut.PlayerInput.Interact.canceled += a => _manager.Running = false;
        _manager.Animator.SetBool("IsRunning", true);
    }

    public override void ExitState()
    {
        _manager.Animator.SetBool("IsRunning", false);
    }

    public override void UpdateState()
    {
        Movement();
        CheckIfSwitchState();
    }
    private void Movement()
    {

        Vector2 moveVector = _manager.InPut.PlayerInput.Movement.ReadValue<Vector2>();
        _manager.MoveDir = new Vector3(moveVector.x, 0, moveVector.y);
        _manager.RB.velocity = _manager.MoveDir * _manager.MoveSpeed;
    }

}