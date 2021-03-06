using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkInvestigate : RAINAction
{
    public checkInvestigate()
    {
        actionName = "checkInvestigate";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
    	
		if(eds.suspects && !eds.isVisionFactorBeyondThreshold())
		{
			eds.chronoBeforeInvestigate += Time.deltaTime;
			
			if(eds.chronoBeforeInvestigate >= eds.waitingBeforeInvestigate)
			{
				ai.WorkingMemory.SetItem("hasToInvestigate", true);
			}				
		}
		
        return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}