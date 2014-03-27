using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class facedDecoyPos : RAINAction
{
    public facedDecoyPos()
    {
        actionName = "facedDecoyPos";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	//Nos hemos acabado de orientar a la posicion del senyuelo. Sopechamos.
    	EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
    	
    	eds.decoyHeard = false;
		ai.WorkingMemory.SetItem("decoyHeard", false);
    	eds.suspects = true;
    	
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}