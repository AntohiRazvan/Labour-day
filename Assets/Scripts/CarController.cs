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
	List<GameObject> WheelMeshes;

	void Start () 
	{
		var guns = GetComponentsInChildren<ConnonController>();
		foreach(var gun in guns)
		{
			gun.enabled = true;
		}
		rigidbody = GetComponent<Rigidbody>();
		directionalWheels = new List<WheelCollider>();
		Wheels = new List<WheelCollider>();
		WheelMeshes = new List<GameObject>();
		var wheels = GetComponentsInChildren<WheelCollider>();
		foreach (var wheel in wheels)
		{
			if(wheel.GetComponent<DirectionalWheel>() != null)
			{
				directionalWheels.Add(wheel);
			}
			Wheels.Add(wheel);
			WheelMeshes.Add(wheel.transform.parent.GetComponentInChildren<MeshRenderer>().gameObject);
		}
	}
	
	void Update () 
	{
		float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");

		for (int i = 0; i < Wheels.Count; i++)
		{
			Wheels[i].brakeTorque = 0;
			if(Input.GetKey(KeyCode.Space))
			{
				Wheels[i].brakeTorque = brakeTorque;
			}
			Wheels[i].motorTorque = verticalInput * motorTorque;
			Vector3 position;
			Quaternion rotation;
			Wheels[i].GetWorldPose(out position, out rotation);
			WheelMeshes[i].transform.position = position;
			WheelMeshes[i].transform.rotation = rotation;
		}

		foreach(var wheel in directionalWheels)
		{
			wheel.steerAngle = horizontalInput * steerAngle;
		}
		
	}
}
