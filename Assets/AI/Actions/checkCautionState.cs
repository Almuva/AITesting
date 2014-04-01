using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkCautionState : RAINAction
{
    public checkCautionState()
    {
        actionName = "checkCautionState";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
		
		bool isInCautionState = eds.attentionDegree == EnemyDataScript.AttentionDegrees.CAUTION
								|| eds.attentionDegree == EnemyDataScript.AttentionDegrees.PERMANENT_CAUTION;
		ai.WorkingMemory.SetItem("isInCautionState", isInCautionState);
		
		return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}