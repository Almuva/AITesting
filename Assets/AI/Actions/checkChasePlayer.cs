using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class checkChasePlayer : RAINAction
{
    public checkChasePlayer()
    {
        actionName = "checkChasePlayer";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	bool hasToChasePlayer = ai.Body.GetComponent<EnemyDataScript>().targetChasePlayer != Vector3.zero;
    	ai.WorkingMemory.SetItem("hasToChasePlayer", hasToChasePlayer);
		ai.WorkingMemory.SetItem("lastPlayerPos", ai.Body.GetComponent<EnemyDataScript>().targetChasePlayer);
    	
        return ActionResult.FAILURE;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}