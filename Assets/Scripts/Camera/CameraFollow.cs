﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float Distance = 5.0f;
	public float DistanceMin = 3.0f;
	public float DistanceMax = 10.0f;    
	private float mouseX = 0.0f;
	private float mouseY = 0.0f;
	private float startingDistance = 0.0f;    
	private float desiredDistance = 0.0f;    
	public float X_MouseSensitivity = 5.0f;
	public float Y_MouseSensitivity = 5.0f;
	public float MouseWheelSensitivity = 5.0f;
	public float Y_MinLimit = -40.0f;
	public float Y_MaxLimit = 80.0f;    
	public float DistanceSmooth = 0.05f;    
	private float velocityDistance = 0.0f;    
	private Vector3 desiredPosition = Vector3.zero;    
	public float X_Smooth;
	public float Y_Smooth;
	private float velX = 0.0f;
	private float velY = 0.0f;
	private float velZ = 0.0f;
	private Vector3 position = Vector3.zero;
   private Transform target; 
 
	void Start() 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		target.gameObject.GetComponent<Rigidbody>().useGravity = true;
		target.gameObject.GetComponent<CarController>().enabled = true;
		foreach(SphereCollider sc in target.gameObject.GetComponentsInChildren<SphereCollider>())
		{
			sc.enabled = false;
		}
		Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
		startingDistance = Distance;
		Reset();
	}
 
	void LateUpdate()
	{
		 if (target == null)
			  return;
         
		 HandlePlayerInput();
         
		 CalculateDesiredPosition();
         
		 UpdatePosition();
	}
 
	void HandlePlayerInput()
	{
		var deadZone = 0.01; // mousewheel deadZone
     
	//	if (Input.GetMouseButton(1))
		{

			 mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
			 mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;
		}

     
		// this is where the mouseY is limited - Helper script
		mouseY = ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);
     
		// get Mouse Wheel Input
		if (Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
		{
			 desiredDistance = Mathf.Clamp(Distance - (Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity), 
																										  DistanceMin, DistanceMax);
		}
	}
 
	void CalculateDesiredPosition()
	{
		//X_Smooth = Time.deltaTime;
		//Y_Smooth = Time.deltaTime;
		// Evaluate distance
		Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velocityDistance, DistanceSmooth);
     
		// Calculate desired position -> Note : mouse inputs reversed to align to WorldSpace Axis
		desiredPosition = CalculatePosition(mouseY, mouseX, Distance);
	}
 
	Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
	{
		var direction = new Vector3(0, 0, -distance);
		var rotation = Quaternion.Euler(rotationX, rotationY, 0);
		return target.position + (rotation * direction);
	}
 
	void  UpdatePosition()
	{
		var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
		var posY = Mathf.SmoothDamp(position.y, desiredPosition.y+1, ref velY, Y_Smooth);
		var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);
		position = new Vector3(posX, posY, posZ);

		transform.position = position;
     
		transform.LookAt(target);
	}
 
	void Reset() 
	{
		mouseX = 0;
		mouseY = 10;
		Distance = startingDistance;
		desiredDistance = Distance;
	}
 
	float ClampAngle(float angle, float min, float max)
	{
		while (angle < -360 || angle > 360)
		{
			if (angle < -360)
					angle += 360;
			if (angle > 360)
					angle -= 360;
		}
     
		return Mathf.Clamp(angle, min, max);
	}
}

