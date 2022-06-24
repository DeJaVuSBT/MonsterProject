using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPushState : PlayerBaseState
{
    public PlayerPushState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void EnterState()
    {
        Debug.Log("Push");
        _manager.Pushing = true;
        _manager.Animator.SetBool("isPushing", true);
        
        if (_manager.transform.position.x > _manager.Target.transform.position.x)
        {
            if (!_manager.GetOldDir)
            {
               _manager.Getfc.SetType = FormChanger.UnitType.NR;
            }
            _manager.transform.localScale = new Vector3(-1, 1, 1);
            _manager.GetOldDir = true;
        }
        else
        {
            if (_manager.GetOldDir)
            {
                _manager.Getfc.SetType = FormChanger.UnitType.NL;
            }
            _manager.transform.localScale = new Vector3(1, 1, 1);
            _manager.GetOldDir = false;
        }
        _manager.Target.GetComponent<MoraEvents>().SoundWhenDrag();
        _manager.Target.transform.SetParent(_manager.transform);
        _manager.InPut.PlayerInput.Interact.canceled += a;
        //_manager.startTutorial(0);
    }

    private void a(InputAction.CallbackContext obj)
    {
         _manager.Pushing = false;
        _manager.Animator.SetBool("isPulling", false);
        _manager.Animator.SetBool("isPushing", false);
    }

    public override void ExitState()
    {
        _manager.Target.transform.SetParent(null);
        _manager.InPut.PlayerInput.Interact.canceled -= a;
        //_manager.endTutorial(2);
        _manager.soundManager.StopSound();
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
    private void Animation() {
        if (_manager.GetOldDir)
        {
            if (_manager.Animator.GetBool("isPushing"))
            {
                if (_manager.MoveDir.x < 0)
                {
                    _manager.Animator.SetBool("isPulling", false);

                }
                else { _manager.Animator.SetBool("isPulling", true); }
            }
            else
            {
                _manager.Animator.SetBool("isPulling", false);
            }
        }
        else
        {
            if (_manager.Animator.GetBool("isPushing"))
            {
                if (_manager.MoveDir.x < 0)
                {
                    _manager.Animator.SetBool("isPulling", true);

                }
                else { _manager.Animator.SetBool("isPulling", false); }
            }
            else
            {
                _manager.Animator.SetBool("isPulling", false);
            }
        }
    }
  
}
