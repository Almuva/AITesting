using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using RAIN.Perception.Sensors;

[RAINAction]
public class updateVisionFactor : RAINAction
{
	private Vector3 playerPos, enemyEyesPos;
	private float 	distance, 
					deltaFactor, 
					mainVisionDistance, 
					periferialVisionDistance, 
					actualVisionFactor;
	
    public updateVisionFactor()
    {
        actionName = "updateVisionFactor";
    }

    public override void Start(AI ai)
    {
        base.Start(ai); 
    }

    public override ActionResult Execute(AI ai)
    {
		bool playerSeenMain = ai.WorkingMemory.GetItem("playerSeenMain").GetValue<GameObject>() != null;
		//if(playerSeenMain) Debug.Log ("MAIN");
		
		bool playerSeenPeriferial = ai.WorkingMemory.GetItem("playerSeenPeriferial").GetValue<GameObject>() != null;
		//if(playerSeenPeriferial) Debug.Log ("PERIFERIAL");
		
		bool playerSensedNear = ai.WorkingMemory.GetItem("playerSensedNear").GetValue<GameObject>() != null;
		//if(playerSensedNear) Debug.Log ("NEAR");
		
		bool playerSensed = playerSeenMain | playerSeenPeriferial | playerSensedNear;
		
		playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		enemyEyesPos = ai.Senses.GetSensor("mainVision").Position;
		distance = Vector3.Distance(playerPos, enemyEyesPos);
		
		//Antes que nada, si estamos en alerta las areas de deteccion sirven para detectar al player al momento.
		if((ai.Body.GetComponent<EnemyDataScript>().attentionDegree == EnemyDataScript.AttentionDegrees.ALERT)
		   && playerSensed)
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
			ai.Body.GetComponent<EnemyDataScript>().addVisionFactor(deltaFactor);
		}
		
		//si vemos al player con el cono de vision periferico, incrementamos el visionFactor un poco (la mitad que si lo vemos con el main)
		if(playerSeenPeriferial)
		{
			periferialVisionDistance = (ai.Senses.GetSensor("periferialVision") as VisualSensor).Range;
			
			//Calculamos cuanto debe incrementarse el deltaFactor segun la distancia a la que este el player
			deltaFactor = 1-(distance / periferialVisionDistance)*0.75f; //se normaliza distancia de 1(enemigo) a 0.25(limite de vision)
			deltaFactor *= Time.deltaTime * 0.5f;
			
			//Modificamos el visionFactor
			ai.Body.GetComponent<EnemyDataScript>().addVisionFactor(deltaFactor);
		}
		
		//si vemos al player con el cono de vision principal, incrementamos el visionFactor bastante
		if(playerSeenMain)
		{
			mainVisionDistance = (ai.Senses.GetSensor("mainVision") as VisualSensor).Range;
			
			//Calculamos cuanto debe incrementarse el deltaFactor segun la distancia a la que este el player
			deltaFactor = 1-(distance / mainVisionDistance)*0.75f; //se normaliza distancia de 1(enemigo) a 0.25(limite de vision)
			deltaFactor *= Time.deltaTime;
			
			//Modificamos el visionFactor
			ai.Body.GetComponent<EnemyDataScript>().addVisionFactor(deltaFactor);
		}
		
		//Si no se ha detectado al player de ninguna de las maneras decrementamos el visionFactor
		if(!playerSensed)
		{
			//asignamos el valor que se descuenta al visionFactor
			deltaFactor = 0.3f*Time.deltaTime;
			
			//Modificamos el visionFactor
			ai.Body.GetComponent<EnemyDataScript>().substractVisionFactor(deltaFactor);
		}
		
		//Si el visionFactor se encuentra en su valor maximo se pasa a ALERT
		if(ai.Body.GetComponent<EnemyDataScript>().visionFactor == 1.0f)
		{
			ai.Body.GetComponent<EnemyDataScript>().attentionDegree = EnemyDataScript.AttentionDegrees.ALERT;
			updateTargetChasePlayer(ai);
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