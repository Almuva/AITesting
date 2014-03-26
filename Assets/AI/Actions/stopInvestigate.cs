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
		eds.lookAts[0] = ai.Body.transform.position + ai.Body.transform.forward;
		eds.lookAts[1] = ai.Body.transform.position + ai.Body.transform.right;
		eds.lookAts[2] = ai.Body.transform.position - ai.Body.transform.forward;
		eds.lookAts[3] = ai.Body.transform.position - ai.Body.transform.right;
		
		eds.timerLookArround = 0.0f;
		eds.timerLookAt = 0.0f;
		
		eds.currentLookAt = eds.lookAts[Random.Range(0,4)];
		
		ai.WorkingMemory.SetItem("hasToLookArround", true);
		ai.WorkingMemory.SetItem("lookAtPoint", eds.currentLookAt);
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}