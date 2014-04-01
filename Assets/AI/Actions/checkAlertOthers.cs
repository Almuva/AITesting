using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkAlertOthers : RAINAction
{
    public checkAlertOthers()
    {
        actionName = "checkAlertOthers";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
    	bool hasToAlertOthers = eds.wantToAlert & eds.canAlert;
		ai.WorkingMemory.SetItem("hasToAlertOthers", hasToAlertOthers);
    
        return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}