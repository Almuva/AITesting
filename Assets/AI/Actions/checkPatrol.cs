using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkPatrol : RAINAction
{
    public checkPatrol()
    {
        actionName = "checkPatrol";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
		if(!eds.suspects)
		{
			ai.WorkingMemory.SetItem("hasToPatrol", true);
		}
		else
		{
			ai.WorkingMemory.SetItem("hasToPatrol", false);
			ai.WorkingMemory.SetItem("currentWPindex", -1);
		}
		
        return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}