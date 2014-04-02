using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class customTimer : RAINAction
{
	private float 	target,
					count;
    public customTimer()
    {
        actionName = "customTimer";
        target = 0.0f;
        count = 0.0f;
    }

    public override void Start(AI ai)
    {
    	target = ai.WorkingMemory.GetItem("customTimer").GetValue<float>();
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	count += Time.deltaTime;
		ai.WorkingMemory.SetItem("customTimerCount", count);
    	
    	if(count >= target)
    	{
    		count = 0.0f;
    		return ActionResult.SUCCESS;
    	}
    	else return ActionResult.RUNNING;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}