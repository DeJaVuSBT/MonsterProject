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
    public PlayerBaseState InteractState()
    {
        return new PlayerInteractState(Manager,this);
    }




}
