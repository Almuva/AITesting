using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkGatekeep : RAINAction
{
    public checkGatekeep()
    {
        actionName = "checkGatekeep";
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
			ai.WorkingMemory.SetItem("hasToGK", true);
		}
		else
		{
			ai.WorkingMemory.SetItem("hasToGK", false);
		}
        
		return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}