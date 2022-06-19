using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStates
{
    NPCManager manager;
   public  NPCStates(NPCManager manager){ this.manager=manager; }

    public NPCStateBase Roaming() {
        return new NPCRoaming(manager,this);
    }
    public NPCStateBase Idle()
    {
        return new NPCIdle(manager, this);
    }
    public NPCStateBase Throw()
    {
        return new NPCThrow(manager, this);
    }
    public NPCStateBase Chase()
    {
        return new NPCChase(manager, this);
    }
    public NPCStateBase ToCage()
    {
        return new NPCToCage(manager, this);
    }
    public NPCStateBase Flee()
    {
        return new NPCFlee(manager, this);
    }
}
