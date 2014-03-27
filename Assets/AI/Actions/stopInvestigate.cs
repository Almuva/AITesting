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
		
		//Podemos mirar en una direccion si hay al menos 2 metros sin que haya una pared o algo
		int validLookAts = 0;
		if(!Physics.Raycast(ai.Body.transform.position, ai.Body.transform.forward, 2.0f))
		{
			eds.lookAts[validLookAts] = ai.Body.transform.position + ai.Body.transform.forward;
			validLookAts++;
		}
		
		if(!Physics.Raycast(ai.Body.transform.position, ai.Body.transform.right, 2.0f))
		{
			eds.lookAts[validLookAts] = ai.Body.transform.position + ai.Body.transform.right;
			validLookAts++;
		}
		
		if(!Physics.Raycast(ai.Body.transform.position, -ai.Body.transform.forward, 2.0f))
		{
			eds.lookAts[validLookAts] = ai.Body.transform.position - ai.Body.transform.forward;
			validLookAts++;
		}
		
		if(!Physics.Raycast(ai.Body.transform.position, -ai.Body.transform.right, 2.0f))
		{
			eds.lookAts[validLookAts] = ai.Body.transform.position - ai.Body.transform.right;
			validLookAts++;
		}
		
		eds.validLookAts = validLookAts;
		
		eds.timerLookArround = 0.0f;
		eds.timerLookAt = 0.0f;
		
		eds.suspects = false;
		eds.chronoBeforeInvestigate = 0.0f;
		
		eds.decoyHeard = false;
		ai.WorkingMemory.SetItem("decoyHeard", false);
		ai.WorkingMemory.SetItem("hasToInvestigate", false);
		
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