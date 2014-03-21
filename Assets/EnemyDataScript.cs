using UnityEngine;
using System.Collections;
using RAIN.Core;

public class EnemyDataScript : MonoBehaviour {

	private AIRig aiRig = null;
	
	public enum AttentionDegrees
	{
		NORMAL = 0,
		CAUTION = 1,
		PERMANENT_CAUTION = 2, 
		ALERT = 3,
		PANIC = 4
	}
	
	public AttentionDegrees attentionDegree;
	
	public float 	visionFactor,
					visionFactorCaution;
					
	public Vector3 targetChasePlayer;
	
	//private RAIN.Memory.MemoryObject debugObject = null;
	//private float debug = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		aiRig = gameObject.GetComponentInChildren<AIRig>();
		aiRig.AI.WorkingMemory.SetItem("player", GameObject.FindGameObjectWithTag("Player"));
		init();
	}
	
	void init()
	{
		visionFactor = 0.0f;
		visionFactorCaution = 0.6f;
		attentionDegree = AttentionDegrees.NORMAL;
		targetChasePlayer = Vector3.zero;
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
	
	/// <summary>
	/// El threshold es el que separa los attentionDegrees NORMAL y CAUTION
	/// </summary>
	public bool isVisionFactorBeyondThreshold()
	{
		return visionFactor >= visionFactorCaution;
	}
	
	public void setTargetChasePlayer()
	{
		targetChasePlayer = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
}
