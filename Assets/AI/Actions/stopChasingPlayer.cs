using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class stopChasingPlayer : RAINAction
{
    public stopChasingPlayer()
    {
        actionName = "stopChasingPlayer";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
		//por si acaso.
    	eds.suspects = false;
    	eds.decoyHeard = false;
    	
    	ai.WorkingMemory.SetItem("decoyHeard", false);
    	eds.canAlert = true;
    	
		eds.targetChasePlayer = Vector3.zero;
		ai.WorkingMemory.SetItem("hasToChangeAD", true);
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}