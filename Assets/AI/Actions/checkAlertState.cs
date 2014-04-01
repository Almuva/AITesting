using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkAlertState : RAINAction
{
    public checkAlertState()
    {
        actionName = "checkAlertState";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
    	
    	bool isInAlertState = eds.attentionDegree == EnemyDataScript.AttentionDegrees.ALERT;
		ai.WorkingMemory.SetItem("isInAlertState", isInAlertState);
    
        return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}