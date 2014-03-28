using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using RAIN.Motion;
using RAIN.Navigation;
using RAIN.Navigation.Waypoints;

[RAINAction]
public class chooseCurrentWP : RAINAction
{
	private MoveLookTarget _currentWP;
	private WaypointSet _wpSet;
	private static int _currentWPindex = -1;
	
    public chooseCurrentWP()
    {
        actionName = "chooseCurrentWP";
    }

    public override void Start(AI ai)
    {
		EnemyDataScript eds = ai.Body.GetComponent<EnemyDataScript>();
    
		//Si no tenemos registrados los waypoints los pillamos
		if (_wpSet == null)
		{
			if(eds.waypointsName == "")
			{
				Debug.Log ("WARNING: patroler with no waypointsName: " + ai.Body.ToString());
			}
			else
			{
				_wpSet = NavigationManager.instance.GetWaypointSet(eds.waypointsName);
				if(_wpSet == null)
				{
					Debug.Log ("WARNING: patroler with no valid waypointsName: " + ai.Body.ToString());
					return;
				}
			}
			
			ai.WorkingMemory.SetItem("currentWPindex", -1);
		}
		
		if(_currentWP == null) _currentWP = new MoveLookTarget();
		
		
		//si no estabamos patrullando en el frame anterior, buscamos waypoint mas cercano para ir hacia el
		_currentWPindex = ai.WorkingMemory.GetItem("currentWPindex").GetValue<int>();
		bool currentWPchanged = false;
		
		if (_currentWPindex == -1)
		{
			_currentWPindex = _wpSet.GetClosestWaypointIndex(ai.Kinematic.Position);
			currentWPchanged = true;
		}
		
		//Si hemos llegado al currentWP (estamos patrullando)...
		if(ai.Motor.IsAtMoveTarget)
		{
			currentWPchanged = true;
			
			//si estabamos patrullando y el indice era el del ultimo nodo pasamos al primero
			if(_currentWPindex == _wpSet.Waypoints.Count - 1) 
			{
				_currentWPindex = 0;
			}
			//sino incrementamos numero de nodo
			else
			{
				_currentWPindex++;
			}
		}
		
		if(currentWPchanged)
		{
			ai.WorkingMemory.SetItem("currentWPindex", _currentWPindex);
			_currentWP.VectorTarget = _wpSet.Waypoints[_currentWPindex].position;
			ai.Motor.moveTarget = _currentWP;
		}
		
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		if (!ai.Motor.IsAtMoveTarget)
		{
			ai.Motor.Move();
			//if (!ai.Animator.IsStatePlaying("walk"))
			//	ai.Animator.StartState("walk");
		}
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}