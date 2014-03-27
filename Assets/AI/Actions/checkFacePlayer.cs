using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkFacePlayer : RAINAction
{
    public checkFacePlayer()
    {
        actionName = "checkFacePlayer";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	//Si estamos en ALERT no hay que quedarse mirando al player
    	if(ai.Body.GetComponent<EnemyDataScript>().attentionDegree == EnemyDataScript.AttentionDegrees.ALERT)
    	{
			ai.WorkingMemory.SetItem("hasToFacePlayer", false);
    	}
    	else
    	{
			bool facingNow = ai.WorkingMemory.GetItem("hasToFacePlayer").GetValue<bool>();
			if(facingNow) return ActionResult.FAILURE;
			
			bool hasToFacePlayer = ai.Body.GetComponent<EnemyDataScript>().isVisionFactorBeyondThreshold();
			ai.WorkingMemory.SetItem("hasToFacePlayer", hasToFacePlayer);
    	}
    	
        return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}