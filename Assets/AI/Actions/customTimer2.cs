using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class customTimer2 : RAINAction
{
	private float 	target,
					count;

	public customTimer2()
	{
		actionName = "customTimer2";
		target = 0.0f;
		count = 0.0f;
	}
	
	public override void Start(AI ai)
	{
		target = ai.WorkingMemory.GetItem("customTimer").GetValue<float>();
		ai.WorkingMemory.SetItem("customTimerEnded", false);
		base.Start(ai);
	}
	
	public override ActionResult Execute(AI ai)
	{
		count += Time.deltaTime;
		
		if(count >= target)
		{
			count = 0.0f;
			ai.WorkingMemory.SetItem("customTimerEnded", true);
			
		}

		return ActionResult.SUCCESS;
	}
	
	public override void Stop(AI ai)
	{
		base.Stop(ai);
	}
}