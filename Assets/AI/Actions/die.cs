using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class die : RAINAction
{
    public die()
    {
        actionName = "die";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
    	//Desactivamos el componente de IA del malo
		ai.Body.GetComponentInChildren<AIRig>().enabled = false;
		ai.Body.GetComponentInChildren<MeshRenderer>().renderer.material.color = Color.red;
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}