using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerScript : MonoBehaviour {

	public Transform targetTransform;
	public float walkVelocity = 1.0f;
	public float runVelocity = 2.0f;
	public float currentVelocity;
	public float rotVelocity = 1.0f;
	public Camera playerCamera;
	
	private Vector3 moveDir;
	private float rotAngle;
	
	public AudioClip[] sfx;
	
	// Use this for initialization
	void Start () {
		currentVelocity = walkVelocity;
	}
	
	void Update()
	{
		moveDir = Vector3.zero;

		if(Input.GetKey(KeyCode.W))
		{
			moveDir += playerCamera.transform.forward;
		}
		if(Input.GetKey(KeyCode.S))
		{
			moveDir -= playerCamera.transform.forward;
		}
		if(Input.GetKey(KeyCode.A))
		{
			moveDir -= playerCamera.transform.right;
		}
		if(Input.GetKey(KeyCode.D))
		{
			moveDir += playerCamera.transform.right;
		}
		moveDir.y = 0;
		moveDir.Normalize();
		
		if(Input.GetKey(KeyCode.LeftShift))
		{
			currentVelocity = runVelocity;
		}
		else
		{
			currentVelocity = walkVelocity;
		}
		
		if(Input.GetKey(KeyCode.E))
		{
			//audio.PlayOneShot(sfx[0]);
			audio.Play();
		}
		
		if(Input.GetKey(KeyCode.M))
		{
			GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
			EnemyDataScript eds = enemy.GetComponentInChildren<EnemyDataScript>();
			eds.die();
		}
		
		//orientar hacia direccion de movimiento
		if(moveDir != Vector3.zero)
		{
			rotAngle = Vector3.Angle(targetTransform.forward, moveDir) * Mathf.Sign(Vector3.Cross(targetTransform.forward, moveDir).y);
			targetTransform.Rotate(targetTransform.up, rotAngle * rotVelocity * Time.deltaTime);
		}
		
	}
	
	void FixedUpdate () 
	{
		GetComponent<CharacterController>().SimpleMove(moveDir * currentVelocity);
	}
}
