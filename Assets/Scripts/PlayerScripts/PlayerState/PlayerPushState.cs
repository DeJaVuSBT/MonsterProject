using UnityEngine;

public class PlayerPushState : PlayerBaseState
{
    public PlayerPushState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void EnterState()
    {
        Debug.Log("Push");
        _manager.Pushing = true;
        _manager.Target.transform.SetParent(_manager.transform);
        _manager.InPut.PlayerInput.Interact.canceled += a => _manager.Pushing = false;
        //_manager.startTutorial(0);
    }

    public override void ExitState()
    {
        _manager.Target.transform.SetParent(null);
        //_manager.endTutorial(2);
    }

    public override void CheckIfSwitchState()
    {
        if (!_manager.Pushing)
        {
            SwitchState(_states.MoveState());
            
        }
    }

    public override void UpdateState()
    {
        Movement();
        CheckIfSwitchState();
        Animation();
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
            _manager.Animator.SetBool("IsWalking", true);

        }
        else { _manager.Animator.SetBool("IsWalking", false); }
    }
}
