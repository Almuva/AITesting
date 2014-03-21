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
		EnemyDataScript.AttentionDegrees ad = ai.Body.GetComponent<EnemyDataScript>().attentionDegree;
    	
    	//Cambiamos el estado de alerta segun el estado actual
    	if(ad == EnemyDataScript.AttentionDegrees.ALERT)
    	{
    		ad = EnemyDataScript.AttentionDegrees.PERMANENT_CAUTION;
    	}
    	else if(ad == EnemyDataScript.AttentionDegrees.CAUTION)
    	{
    		ad = EnemyDataScript.AttentionDegrees.NORMAL;
    	}
    	
		ai.Body.GetComponent<EnemyDataScript>().attentionDegree = ad;
    	
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}