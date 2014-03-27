using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class lookArround : RAINAction
{
    public lookArround()
    {
        actionName = "lookArround";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	//Tenemos 4 vector3 con 4 direcciones diferentes a las que mirar.
    	EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
    	
    	//Avanzar timers
		eds.timerLookArround += Time.deltaTime;
		eds.timerLookAt += Time.deltaTime;
    	
    	//Si ha terminado el tiempo de mirar al rededor acabamos la accion
    	if(eds.timerLookArround >= eds.lookArroundTime)
    	{
			ai.WorkingMemory.SetItem("hasToLookArround", false);
			ai.WorkingMemory.SetItem("hasToChangeAD", true);
    	}
 
 		//Terminamos de mirar en una de las direcciones. Hay que escoger otra.
		if(eds.timerLookAt >= eds.lookAtTime)
    	{
			eds.timerLookAt = 0.0f;
			eds.currentLookAt = eds.lookAts[Random.Range(0,eds.validLookAts)];
			ai.WorkingMemory.SetItem("lookAtPoint", eds.currentLookAt);
    	}
    	
    	return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}