using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerOptioning: PlayerBaseState
{
    private bool optioning = true;
    private int leftorRight = 0;
    public PlayerOptioning(PlayerStateManager manager, PlayerState states) : base(manager, states) { }
    public override void CheckIfSwitchState()
    {
        if (leftorRight!=0&&optioning)
        {
            _manager.InPut.EventInput.Disable();
            _manager.Target.GetComponent<MoraEvents>().Selected=leftorRight;
            _manager.Target.GetComponent<MoraEvents>().SelectedAnimation();
            optioning = false;
        }
        if (optioning == false)
        {
            if (_manager.Target.GetComponent<MoraEvents>().selectedAnimationDone)
            {
                SwitchStateByOption();
            } 
        }
        
    }

    public override void EnterState()
    {
        Debug.Log("Optioning");
        _manager.Target.GetComponent<MoraEvents>().ShowOption();
        _manager.SwitchToEventInput();
        _manager.InPut.EventInput.Button4.started += Button4_started => leftorRight = 2;  
        _manager.InPut.EventInput.Button3.started += Button3_started => leftorRight = 1; 

        //_manager.startTutorial(2);
    }

    public override void ExitState()
    {

        //disable ui
        _manager.InPut.EventInput.Enable();
        _manager.Target.GetComponent<MoraEvents>().HideOption();
    }

    public override void UpdateState()
    {
        CheckIfSwitchState();
    }


    private void SwitchStateByOption() {
            if (leftorRight == 1)
            {
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
            else if (leftorRight == 2)
            {
                switch (_manager.Target.GetComponent<MoraEvents>().GetInteractType())
                {
                    case 0:
                        SwitchState(_states.ShakeState());
                        break;
                    case 1:
                        SwitchState(_states.RotateState());
                        break;
                    case 2:
                        SwitchState(_states.SmashState());
                        break;
                    default:
                        break;

                }
            }
            else { Debug.Log("Error,selected but neither good/bad"); }
        }
    

}

