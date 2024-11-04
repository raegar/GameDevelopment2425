using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateInitializeFromLoad : BaseState
{
   public List<GameObject> systemsToInstanciate;



    public override void StateEnter()
    {
        base.StateEnter();
        Debug.Log("StateInitializeFromLoad OnEnter");
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log("StateInitializeFromLoad Update");
    }

    public override void StateExit()
    {
        base.StateExit();
        Debug.Log("StateInitializeFromLoad OnExit");
    }
}
