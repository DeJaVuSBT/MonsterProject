using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private Transform target;
    public  int interactRange = 4;
    public PlayerMoveState(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckIfSwitchState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        ClosedColliderAround();
        Movement();
    }
    private void MoveInpute() { 
        
    }
    private void Movement()
    {
          
            Vector2 moveVector = _manager.InPut.PlayerInput.Movement.ReadValue<Vector2>();
             _manager.MoveDir = new Vector3(moveVector.x, 0, moveVector.y);
        _manager.RB.velocity = _manager.MoveDir * _manager.MoveSpeed;
    }

    private GameObject ClosedColliderAround( )
    {
        Vector3 offset = new Vector3(1, 1, 1);
        Vector3 playerPos = _manager.transform.position;
        //Vector3 playerhead = transform.position + new Vector3(0, 0.5f, -1f);
        Collider[] ColliderAround = Physics.OverlapSphere(playerPos+offset, interactRange);

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
            return Closest.transform.gameObject;
        }

        return default;
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }
}
