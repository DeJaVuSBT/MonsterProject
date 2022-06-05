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
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        Animation();
        Movement();
        CheckIfSwitchState();
    }
    private void Movement()
    {

        Vector2 moveVector = _manager.InPut.PlayerInput.Movement.ReadValue<Vector2>();
        _manager.MoveDir = new Vector3(moveVector.x, 0, moveVector.y);
        _manager.RB.velocity = _manager.MoveDir * _manager.MoveSpeed;
    }
    private void Animation()
    {

        if (_manager.MoveDir != Vector3.zero)
        {
            _manager.Animator.SetBool("New Bool", true);

        }
        else { _manager.Animator.SetBool("New Bool", false); }


    }
}