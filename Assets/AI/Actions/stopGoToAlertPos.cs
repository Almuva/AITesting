using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class stopGoToAlertPos : RAINAction
{
	public stopGoToAlertPos()
    {
		actionName = "stopGoToAlertPos";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
		eds.alertedByOther = false;
		ai.WorkingMemory.SetItem("hasToAnswerAlert", false);
		ai.WorkingMemory.SetItem("hasToChangeAD", true);
		eds.prepareLookAts();
		ai.WorkingMemory.SetItem("hasToLookArround", true);
		ai.WorkingMemory.SetItem("lookAtPoint", eds.currentLookAt);
    
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}