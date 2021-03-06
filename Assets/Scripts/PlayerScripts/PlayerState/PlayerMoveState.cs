using UnityEngine;
public class PlayerMoveState : PlayerBaseState
{
    private bool switched = false;
    private GameObject outLineTarget;
    public PlayerMoveState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void EnterState()
    {
        _manager.InPut.PlayerInput.Enable();
        //_manager.arrow.SetActive(true);
        Debug.Log("Moves");
        _manager.ApplyWalkSpeed();
    }

    public override void CheckIfSwitchState()
    {
        //set interact target
        if (ClosedColliderAround() && ClosedColliderAround().GetComponent<MoraEvents>())
        {
            //target switched!
            GameObject preTarget = _manager.Target;
            _manager.Target = ClosedColliderAround();

            if (preTarget != _manager.Target)
            {
                _manager.SwitchedTarget();
            }

        }
        else { _manager.Target = null; _manager.DestoryOutLinedTarget(); }



        if (_manager.Target != null)
        {
            if (_manager.InPut.PlayerInput.Interact.IsPressed())
            {
                if (!_manager.Target.GetComponent<MoraEvents>().doubleInteraction)
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
                else
                {
                    _manager.Target.GetComponent<Interactable>().Interact();
                    SwitchState(_states.OptionState());
                }

            }
        }
        else if (_manager.Target == null)
        {
            if (_manager.InPut.PlayerInput.Interact.IsPressed() && _manager.MoveDir != Vector3.zero)
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
         ShowArrow();
    }
    private void Animation()
    {

        if (_manager.MoveDir != Vector3.zero)
        {
            _manager.Animator.SetBool("IsWalking", true);

        }
        else { _manager.Animator.SetBool("IsWalking", false); }
    }
    private void Movement()
    {

        Vector2 moveVector = _manager.InPut.PlayerInput.Movement.ReadValue<Vector2>();
        _manager.MoveDir = new Vector3(moveVector.x, 0, moveVector.y);
        _manager.RB.velocity = _manager.MoveDir * _manager.MoveSpeed;
    }

    private GameObject ClosedColliderAround()
    {
        Vector3 offset = new Vector3(0, 1, 0.7f);
        Vector3 playerPos = _manager.transform.position;
        Collider[] ColliderAround = Physics.OverlapSphere(playerPos + offset, _manager.GetInteractRange);

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
            if (Closest != null)
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
    private void ShowArrow()
    {
        if (_manager.HBar.GetMoralAmount()<=50)
        {

            GameObject preobj = ClosedColliderAroundArrow();
            if (!switched)
            {
                GameObject copyfromtarget = preobj.GetComponentInChildren<MeshRenderer>().gameObject;
                GameObject CreatedoutlineObject = Object.Instantiate(copyfromtarget, preobj.transform);
                Debug.Log("created one outline");
                CreatedoutlineObject.GetComponent<Renderer>().material = _manager.OutLineA;
                CreatedoutlineObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                if (outLineTarget == null)
                {
                    outLineTarget = CreatedoutlineObject;
                }
                else
                {
                    Object.Destroy(outLineTarget);
                    outLineTarget = CreatedoutlineObject;
                }
                switched = true;
            }

            if (preobj != outLineTarget)
            {
                switched = false;
            }
               
        }

    }
    private GameObject ClosedColliderAroundArrow()
    {
        Vector3 offset = new Vector3(0, 1, 0.7f);
        Vector3 playerPos = _manager.transform.position;
        Collider[] ColliderAround = Physics.OverlapSphere(playerPos + offset, 10);

        if (ColliderAround.Length > 1)
        {
            Collider Closest = null;
            for (int i = 0; i < ColliderAround.Length; i++)
            {
                if (ColliderAround[i].GetComponent<MoraEvents>())
                {
                    if (ColliderAround[i].GetComponent<MoraEvents>().GetHunger!=0)
                    {
                        if (Closest == null)
                        {
                            Closest = ColliderAround[i];
                        }
                        else
                        {
                            if (Vector3.Distance(playerPos, Closest.transform.position) > Vector3.Distance(playerPos, ColliderAround[i].transform.position))
                            {
                                Closest = ColliderAround[i];
                            }

                        }
                    }

                }
            }
            if (Closest != null)
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
        _manager.Animator.SetBool("IsWalking", false);
        //_manager.arrow.SetActive(false);
        if (outLineTarget!=null)
        {
            Object.Destroy(outLineTarget);
        }

    }
}
