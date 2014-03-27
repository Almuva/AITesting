using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class getGKinfo : RAINAction
{
    public getGKinfo()
    {
        actionName = "getGKinfo";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	//obtener punto de gatekeep y punto al que mirar desde ahi
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
		ai.WorkingMemory.SetItem("GKpos", eds.InitPos);
		ai.WorkingMemory.SetItem("GKlookTo", eds.InitLookTo);
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}