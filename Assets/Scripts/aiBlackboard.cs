using UnityEngine;
using System.Collections;

public class aiBlackboard : MonoSingleton<aiBlackboard> {

	public Vector3 alertPos;

	public override void Init()
	{
		alertPos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setAlertPos(Vector3 ap)
	{
		alertPos = ap;
	}
	
	public Vector3 getAlertPos()
	{
		return alertPos;
	}
}
