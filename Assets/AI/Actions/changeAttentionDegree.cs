using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class changeAttentionDegree : RAINAction
{
    public changeAttentionDegree()
    {
        actionName = "changeAttentionDegree";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
    	
    	//Cambiamos el estado de alerta segun el estado actual
		if(eds.attentionDegree == EnemyDataScript.AttentionDegrees.ALERT)
    	{
			eds.attentionDegree = EnemyDataScript.AttentionDegrees.PERMANENT_CAUTION;
    	}
		else if(eds.attentionDegree == EnemyDataScript.AttentionDegrees.CAUTION)
    	{
			eds.attentionDegree = EnemyDataScript.AttentionDegrees.NORMAL;
    	}
		
		eds.suspects = false;
		eds.chronoBeforeInvestigate = 0.0f;
		eds.targetChasePlayer = Vector3.zero;
    	
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}