using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class debugAction : RAINAction
{
    public debugAction()
    {
        actionName = "debugAction";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	//RAIN.Memory.MemoryObject MO = ai.WorkingMemory.GetItem("target");
    	//Debug.Log (MO.ToString());
    	
    	Debug.Log ("HEY!!");
        return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}