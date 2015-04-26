using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour 
{
	[SerializeField]
	float motorTorque;
	[SerializeField]
	float steerAngle;
	[SerializeField]
	float brakeTorque;

	Rigidbody rigidbody;
	List<WheelCollider> directionalWheels;
	List<WheelCollider> Wheels;

	void Start () 
	{
		rigidbody = GetComponent<Rigidbody>();
		directionalWheels = new List<WheelCollider>();
		Wheels = new List<WheelCollider>();
		var wheels = GetComponentsInChildren<WheelCollider>();
		foreach (var wheel in wheels)
		{
			if(wheel.GetComponent<DirectionalWheel>() != null)
			{
				directionalWheels.Add(wheel);
			}
			Wheels.Add(wheel);
		}
	}
	
	void Update () 
	{
		float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");

		foreach (var wheel in Wheels)
		{
			wheel.brakeTorque = 0;
			if(Input.GetKey(KeyCode.Space))
			{
				wheel.brakeTorque = brakeTorque;
			}
			wheel.motorTorque = verticalInput * motorTorque;
		}

		foreach(var wheel in directionalWheels)
		{
			wheel.steerAngle = horizontalInput * steerAngle;
		}
		
	}
}
