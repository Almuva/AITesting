using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {

	public Transform targetTransform;
	public float velocity = 1.0f;
	public float rotVelocity = 1.0f;
	public Camera playerCamera;
	
	private Vector3 moveDir;
	private float rotAngle;
	
	// Use this for initialization
	void Start () {
	
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
		
		//orientar hacia direccion de movimiento
		if(moveDir != Vector3.zero)
		{
			rotAngle = Vector3.Angle(targetTransform.forward, moveDir) * Mathf.Sign(Vector3.Cross(targetTransform.forward, moveDir).y);
			targetTransform.Rotate(targetTransform.up, rotAngle * rotVelocity * Time.deltaTime);
		}
		
	}
	
	void FixedUpdate () 
	{
		GetComponent<CharacterController>().SimpleMove(moveDir * velocity);
	}
}
