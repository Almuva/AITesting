using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using RAIN.Perception.Sensors;

[RAINAction]
public class updateSenses : RAINAction
{
	private Vector3 playerPos, enemyEyesPos;
	private float 	distance, 
					deltaFactor, 
					mainVisionDistance, 
					periferialVisionDistance, 
					actualVisionFactor;
	
	private EnemyDataScript eds;
	
	public updateSenses()
	{
		actionName = "updateSenses";
	}
	
	public override void Start(AI ai)
	{
		base.Start(ai);
		eds = ai.Body.GetComponent<EnemyDataScript>();
	}
	
	public override ActionResult Execute(AI ai)
	{
	//COMPROBAR CON QUE SENSORES DETECTAMOS AL PLAYER
		bool playerSeenMain = ai.WorkingMemory.GetItem("playerSeenMain").GetValue<GameObject>() != null;
		//if(playerSeenMain) Debug.Log ("MAIN");
		
		bool playerSeenPeriferial = ai.WorkingMemory.GetItem("playerSeenPeriferial").GetValue<GameObject>() != null;
		//if(playerSeenPeriferial) Debug.Log ("PERIFERIAL");
		
		bool playerSensedNear = ai.WorkingMemory.GetItem("playerSensedNear").GetValue<GameObject>() != null;
		//if(playerSensedNear) Debug.Log ("NEAR");
		
		bool playerSensed = playerSeenMain | playerSeenPeriferial | playerSensedNear;
		eds.isSeeingPlayer = playerSeenMain | playerSeenPeriferial;
		
		//Sonido
		bool decoyHeardNow = ai.WorkingMemory.GetItem("decoySensed").GetValue<GameObject>() != null;
		
		
	//MODIFICAR VISIONFACTOR
		playerPos = eds.getPlayer().transform.position;
		enemyEyesPos = ai.Senses.GetSensor("mainVision").Position;
		distance = Vector3.Distance(playerPos, enemyEyesPos);
		
		//Antes que nada, si estamos en alerta las areas de deteccion sirven para detectar al player al momento.
		if((eds.attentionDegree == EnemyDataScript.AttentionDegrees.ALERT)  &&  playerSensed)
		{
			updateTargetChasePlayer(ai);
			return ActionResult.SUCCESS;
		}
		
		//si el player esta dentro de nuestro "espacio vital" incrementamos un poco el visionFactor
		if(playerSensedNear)
		{
			//Lo hacemos simple: como el near es bastante cerca, incrementamos linealmente el visionFactor
			deltaFactor = Time.deltaTime*0.2f;
			
			//Modificamos el visionFactor
			eds.addVisionFactor(deltaFactor);
		}
		
		//si vemos al player con el cono de vision periferico, incrementamos el visionFactor un poco (la mitad que si lo vemos con el main)
		if(playerSeenPeriferial)
		{
			periferialVisionDistance = (ai.Senses.GetSensor("periferialVision") as VisualSensor).Range;
			
			//Calculamos cuanto debe incrementarse el deltaFactor segun la distancia a la que este el player
			deltaFactor = 1-(distance / periferialVisionDistance)*0.75f; //se normaliza distancia de 1(enemigo) a 0.25(limite de vision)
			deltaFactor *= Time.deltaTime * 0.5f;
			
			//Modificamos el visionFactor
			eds.addVisionFactor(deltaFactor);
		}
		
		//si vemos al player con el cono de vision principal, incrementamos el visionFactor bastante
		if(playerSeenMain)
		{
			mainVisionDistance = (ai.Senses.GetSensor("mainVision") as VisualSensor).Range;
			
			//Calculamos cuanto debe incrementarse el deltaFactor segun la distancia a la que este el player
			deltaFactor = 1-(distance / mainVisionDistance)*0.75f; //se normaliza distancia de 1(enemigo) a 0.25(limite de vision)
			deltaFactor *= Time.deltaTime;
			
			//Modificamos el visionFactor
			eds.addVisionFactor(deltaFactor);
		}
		
		//Si no se ha detectado al player de ninguna de las maneras decrementamos el visionFactor
		if(!playerSensed)
		{
			//asignamos el valor que se descuenta al visionFactor
			deltaFactor = 0.3f*Time.deltaTime;
			
			//Modificamos el visionFactor
			eds.substractVisionFactor(deltaFactor);
		}
		else
		{
			if(!eds.suspects) eds.chronoBeforeInvestigate = 0.0f;
			
			if(eds.isVisionFactorBeyondThreshold())
			{
				eds.lastPointSensed = eds.getPlayer().transform.position;
				ai.WorkingMemory.SetItem("lastPointSensed", eds.lastPointSensed);
			}
		}
		
	//MODIFICACION DEL ATTENTIONDEGREE
		//Si el visionFactor se encuentra en su valor maximo se pasa a ALERT
		if(eds.visionFactor == 1.0f)
		{
			eds.setAttentionDegree(EnemyDataScript.AttentionDegrees.ALERT);
			updateTargetChasePlayer(ai);
		}
		else if (eds.isVisionFactorBeyondThreshold())
		{
			eds.suspects = true;
			eds.decoyHeard = false;
			ai.WorkingMemory.SetItem("decoyHeard", false);
			
			//Las siguientes 2 lineas son solo para patrulleros (no pasa nada si estan en los demas. Seran 2 variables que no se usaran)
			ai.WorkingMemory.SetItem("hasToPatrol", false);
			ai.WorkingMemory.SetItem("currentWPindex", -1);
			
			if(eds.attentionDegree != EnemyDataScript.AttentionDegrees.PERMANENT_CAUTION
			   && eds.attentionDegree != EnemyDataScript.AttentionDegrees.ALERT)
			{
				eds.setAttentionDegree(EnemyDataScript.AttentionDegrees.CAUTION);
			}
		}
		else if(decoyHeardNow && eds.attentionDegree != EnemyDataScript.AttentionDegrees.ALERT)
		{
			if(eds.attentionDegree == EnemyDataScript.AttentionDegrees.NORMAL) 
				eds.setAttentionDegree(EnemyDataScript.AttentionDegrees.CAUTION);
			eds.decoyHeard = true;
			ai.WorkingMemory.SetItem("decoyHeard", true);
			eds.lastPointSensed = eds.getPlayer().transform.position;
			ai.WorkingMemory.SetItem("lastPointSensed", eds.lastPointSensed);
		}
		
		return ActionResult.SUCCESS;
	}
	
	public override void Stop(AI ai)
	{
		base.Stop(ai);
	}
	
	private void updateTargetChasePlayer(AI ai)
	{
		ai.Body.GetComponent<EnemyDataScript>().setTargetChasePlayer();
	}
}