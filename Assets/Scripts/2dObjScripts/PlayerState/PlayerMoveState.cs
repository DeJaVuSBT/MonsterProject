using UnityEngine;
using System;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void EnterState()
    {
        Debug.Log("Moves");
        _manager.ApplyWalkSpeed();
    }

    public override void CheckIfSwitchState()
    {
        //set interact target
        if (ClosedColliderAround()&& ClosedColliderAround().GetComponent<MoraEvents>())
        {
            //target switched!
            GameObject preTarget=_manager.Target;
           _manager.Target = ClosedColliderAround();

            if (preTarget!=_manager.Target)
            {
                _manager.SwitchedTarget();
            }
            
            
        }
        else { _manager.Target = null; _manager.DestoryOutLinedTarget(); }



        if ( _manager.Target != null)
        {
            if (_manager.InPut.PlayerInput.Interact.IsPressed())
            {
                _manager.Target.GetComponent<Interactable>().Interact();
                switch (_manager.Target.GetComponent<MoraEvents>().GetInteractType())
                {
                    case 0:
                        SwitchState(_states.ShakeState());
                        break;
                    case 1:
                        SwitchState(_states.RotateState());
                        break;
                    case 2:
                        SwitchState(_states.PushState());
                        break;
                    case 3:
                        SwitchState(_states.SmashState());
                        break;
                    default:
                        break;

                }
            }
        }
        else if(_manager.Target==null)
        {
            if (_manager.InPut.PlayerInput.Interact.IsPressed())
            {
                SwitchState(_states.RunState());
       
            }
        }
    }

    public override void UpdateState()
    {
        CheckIfSwitchState();
        Movement();
        Animation();
    }
    private void Animation()
    {

        if (_manager.MoveDir != Vector3.zero)
        {
            _manager.Animator.SetBool("New Bool", true);

        }
        else { _manager.Animator.SetBool("New Bool", false); }


    }
    private void Movement()
    {
          
        Vector2 moveVector = _manager.InPut.PlayerInput.Movement.ReadValue<Vector2>();
        _manager.MoveDir = new Vector3(moveVector.x, 0, moveVector.y);
        _manager.RB.velocity = _manager.MoveDir * _manager.MoveSpeed;
    }

    private GameObject ClosedColliderAround( )
    {
        Vector3 offset = new Vector3(0, 1, 0.7f);
        Vector3 playerPos = _manager.transform.position;
        Collider[] ColliderAround = Physics.OverlapSphere(playerPos+offset, _manager.GetInteractRange);

        if (ColliderAround.Length > 1)
        {
            Collider Closest = null;
            for (int i = 0; i < ColliderAround.Length; i++)
            {
                if (ColliderAround[i].gameObject == _manager.gameObject)
                {
                    continue;
                }
                if (Closest == null)
                {
                    Closest = ColliderAround[i];
                    continue;
                }
                if (Vector3.Distance(playerPos, Closest.transform.position) > Vector3.Distance(playerPos, ColliderAround[i].transform.position))
                {
                    Closest = ColliderAround[i];
                }
            }
            if (Closest!=null)
            {
                return Closest.transform.gameObject;
            }
            else
            {
                return null;
            }
           
        }
        else { return null; }

      
    }

    public override void ExitState()
    {
        _manager.RB.velocity = Vector3.zero;
        _manager.InteractIcon.SetActive(false);
    }
}
