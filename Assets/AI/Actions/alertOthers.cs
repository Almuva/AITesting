using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class alertOthers : RAINAction
{
    public alertOthers()
    {
        actionName = "alertOthers";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
		eds.soundAlertShout.audio.Play();
		eds.wantToAlert = false;
		eds.canAlert = false;
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}
