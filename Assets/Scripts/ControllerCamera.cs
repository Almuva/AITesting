using UnityEngine;
using System.Collections;

public class ControllerCamera : MonoBehaviour {

	public Camera myCamera;
	public Transform playerTransform;
	public float distance;
	public float x_offset, y_offset;
	public float mSpeed = 1.0f;
	public float yMinLimit, yMaxLimit;
	
	private Vector3 playerPos;
	private Vector3 mousePosBefore;
	private float cam_x, cam_y;
	private Quaternion rotation;
	
	// Use this for initialization
	void Start () {
		//Camara detras del player, mirando hacia el
		playerPos = playerTransform.position;
		Vector3 target = playerPos - x_offset*myCamera.transform.right + y_offset*myCamera.transform.up;
		myCamera.transform.position = target - playerTransform.forward*distance;
		myCamera.transform.LookAt( target );
		
		//guardamos euler angles actuales
		Vector3 e_angles = transform.eulerAngles;
		cam_x = e_angles.y;
		cam_y = e_angles.x;
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = playerTransform.position;
		
		//Modificar direccion de la camara segun input de mouse
		cam_x += Input.GetAxis("Mouse X") * mSpeed * Time.deltaTime;
		cam_y -= Input.GetAxis("Mouse Y") * mSpeed * Time.deltaTime;
		cam_y = ClampAngle(cam_y, yMinLimit, yMaxLimit);
		myCamera.transform.rotation = Quaternion.Euler(cam_y, cam_x, 0);
		
		//Modificar pos de la camara segun su rotation y offsets
		Vector3 target = playerPos - x_offset*myCamera.transform.right + y_offset*myCamera.transform.up;
		myCamera.transform.position = target - myCamera.transform.forward * distance;
	}
	
	static float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
