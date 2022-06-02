using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
   PlayerStateManager Manager;
    public PlayerState(PlayerStateManager manager) { 
        Manager= manager;
    }
    public PlayerBaseState MoveState() {
        return new PlayerMoveState(Manager,this);
    }
    public PlayerBaseState PushState()
    {
        return new PlayerPushState(Manager,this);
    }
    public PlayerBaseState ShakeState()
    {
        return new PlayerShakeState(Manager, this);
    }
    public PlayerBaseState RotateState()
    {
        return new PlayerRotateState(Manager, this);
    }

    public PlayerBaseState RunState()
    {
        return new PlayerRunState(Manager, this);
    }
    public PlayerBaseState SmashState()
    {
        return new PlayerSmashButton(Manager, this);
    }

}
