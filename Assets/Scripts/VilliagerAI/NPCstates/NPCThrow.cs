using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCThrow : NPCStateBase
{
    private float ticker = 0.0f;
    private float attackTime = 1f;
    private bool nowthrow = false;
    public NPCThrow(NPCManager manager, NPCStates states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("AI throw");
        _manager.Animator.SetBool("IsThrowing", true);
        TimerAction.Create(() => _manager.Animator.SetBool("IsThrowing", false), 0.2f);
        TimerAction.Create(()=> nowthrow = true, 0.2f);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (nowthrow)
        {
            ThrowPlayer();
        }
    }

    private void ThrowPlayer()
    {
        
        _manager.Stone.SetActive(true);

        ticker += Time.deltaTime;
        float t = ticker / attackTime;
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        Vector3 p1 = _manager.transform.position;
        //0 is headposition 
        Vector3 p3 = _manager.Player.transform.GetChild(0).position;
        Vector3 p2 = (p3 - p1) / 2 + p1 + new Vector3(0, 2f, 0);
        
        Vector3 currPos = ThrowCurve.GetCurvePoint(p1, p2, p3, t);
        _manager.Stone.transform.position = currPos;

        if (t >= 1.0f)
        {
            _manager.Stone.GetComponent<Rigidbody>().velocity = Vector3.down*4;
            TimerAction.Create(() => _manager.Stone.GetComponent<Rigidbody>().velocity=Vector3.zero, 0.2f);
            TimerAction.Create(() => _manager.Stone.SetActive(false), 2f);
            TimerAction.Create(() => SwitchState(_states.Idle()), 3f); 
            nowthrow = false;
            _manager.Player.GetComponent<PlayerStateManager>().BeingCaught();
            TimerAction.Create(() => _manager.MBar.Reset(), 3f);
        }
    }
}
