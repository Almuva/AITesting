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
			eds.setAttentionDegree(EnemyDataScript.AttentionDegrees.PERMANENT_CAUTION);
    	}
		else if(eds.attentionDegree == EnemyDataScript.AttentionDegrees.CAUTION)
    	{
			eds.setAttentionDegree(EnemyDataScript.AttentionDegrees.NORMAL);
    	}
		
		ai.WorkingMemory.SetItem("hasToChangeAD", false);
    	
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}