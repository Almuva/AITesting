using UnityEngine;
using System.Collections;
using RAIN.Core;

public class EnemyDataScript : MonoBehaviour {

	private AIRig aiRig = null;
	private GameObject player; 
	
	public enum AttentionDegrees
	{
		NORMAL = 0,
		CAUTION = 1,
		PERMANENT_CAUTION = 2, 
		ALERT = 3,
		PANIC = 4
	}
	
	public GameObject soundAlertShout;
	
	public AttentionDegrees attentionDegree;
	
	public bool isSeeingPlayer,
				suspects,
				decoyHeard,
				wantToAlert,
				canAlert;
	
	public float 	visionFactor,
					visionFactorCaution,
					chronoBeforeInvestigate,
					waitingBeforeInvestigate,
					timerLookArround,
					lookArroundTime,	
					timerLookAt,
					lookAtTime,
					patrolerStopTime,
					patrolerStopTimeBase,
					patrolerStopTimeRnd,
					patrolerStopTimer,
					currentVel,
					normalVel,
					cautionVel,
					alertVel;
					
	public Vector3 targetChasePlayer,
					lastPointSensed,
					currentLookAt;
					
	public Vector3[] lookAts;
	public int validLookAts;
	
	private Vector3 initPos,
					initLookTo;
					
	public Vector3 InitPos
	{
		get{  return initPos;  }
	}
	
	public Vector3 InitLookTo
	{
		get{  return initLookTo;  }
	}
	
	public string waypointsName = ""; //Solo para patrulleros
	
	//private RAIN.Memory.MemoryObject debugObject = null;
	//private float debug = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		aiRig = gameObject.GetComponentInChildren<AIRig>();
		player = GameObject.FindGameObjectWithTag("Player");
		aiRig.AI.WorkingMemory.SetItem("player", player);
		lookAts = new Vector3[4];
		initPos = transform.position;
		initLookTo = transform.position + transform.forward;
		
		//normalVel = 1.0f;
		//cautionVel = 2.0f;
		//alertVel = 3.0f;
		
		init();
	}
	
	void init()
	{
		setAttentionDegree(AttentionDegrees.NORMAL);
		
		isSeeingPlayer = false;
		suspects = false;
		decoyHeard = false;
		wantToAlert = false;
		canAlert = true;
		
		visionFactor = 0.0f;
		visionFactorCaution = 0.6f;
		chronoBeforeInvestigate = 0.0f;
		waitingBeforeInvestigate = 3.0f;
		timerLookArround = 0.0f;
		//lookArroundTime = 5.0f;	
		timerLookAt = 0.0f;
		//lookAtTime = 2.0f;
		resetPatrolerStopTime();
		currentVel = normalVel;
		
		targetChasePlayer = Vector3.zero;
		lastPointSensed = Vector3.zero;
		lookAts[0] = Vector3.zero;
		lookAts[1] = Vector3.zero;
		lookAts[2] = Vector3.zero;
		lookAts[3] = Vector3.zero;
		currentLookAt = Vector3.zero;
		validLookAts = 0;
		
		aiRig.AI.WorkingMemory.SetItem("hasToChasePlayer", false);
		aiRig.AI.WorkingMemory.SetItem("hasToInvestigate", false);
		aiRig.AI.WorkingMemory.SetItem("hasToFacePlayer", false);
		aiRig.AI.WorkingMemory.SetItem("hasToLookArround", false);		
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	public void addVisionFactor(float vf_delta)
	{
		if(vf_delta < 0.0f) return;
		visionFactor = Mathf.Clamp(visionFactor+vf_delta, 0.0f, 1.0f);
	}
	
	public void substractVisionFactor(float vf_delta)
	{
		if(vf_delta < 0.0f) return;
		visionFactor = Mathf.Clamp(visionFactor-vf_delta, 0.0f, 1.0f);
	}
	
	public void setAttentionDegree(AttentionDegrees ad)
	{
		switch(ad)
		{
			case AttentionDegrees.NORMAL:
				currentVel = normalVel;
				break;
			case AttentionDegrees.CAUTION:
			case AttentionDegrees.PERMANENT_CAUTION:
				currentVel = cautionVel;
				break;
			case AttentionDegrees.ALERT:
				currentVel = alertVel;
				break;
			case AttentionDegrees.PANIC:
				currentVel = 0.0f;
				break;
		}
		
		attentionDegree = ad;
		aiRig.AI.WorkingMemory.SetItem("currentVel", currentVel);
	}
	
	/// <summary>
	/// El threshold es el que separa los attentionDegrees NORMAL y CAUTION
	/// </summary>
	public bool isVisionFactorBeyondThreshold()
	{
		return visionFactor >= visionFactorCaution;
	}
	
	public void setTargetChasePlayer()
	{
		targetChasePlayer = player.transform.position;
	}
	
	public GameObject getPlayer()
	{
		return player;
	}
	
	public void resetPatrolerStopTime()
	{
		patrolerStopTime = patrolerStopTimeBase + Random.Range(0.0f, patrolerStopTimeRnd);
		patrolerStopTimer = 0.0f;
	}
	
	public void die()
	{
		aiRig.AI.WorkingMemory.SetItem("hasToDie", true);
	}
}
