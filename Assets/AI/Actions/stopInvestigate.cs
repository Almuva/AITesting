using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class stopInvestigate : RAINAction
{
    public stopInvestigate()
    {
        actionName = "stopInvestigate";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		//Hemos llegado a un lugar sospechoso. Hay que guardar los datos para mirar al rededor
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
		
		eds.prepareLookAts();
		
		eds.suspects = false;
		eds.chronoBeforeInvestigate = 0.0f;
		
		eds.decoyHeard = false;
		ai.WorkingMemory.SetItem("decoyHeard", false);
		ai.WorkingMemory.SetItem("hasToInvestigate", false);
		
		ai.WorkingMemory.SetItem("hasToLookArround", true);
		ai.WorkingMemory.SetItem("lookAtPoint", eds.currentLookAt);
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}