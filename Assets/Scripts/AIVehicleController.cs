using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIVehicleController : MonoBehaviour 
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
	GameObject target;

	float horizontalInputDelay = 0.5f;
	float lastHorizontalInput = 0;
	float lastAngle;
	float direction = 0;
	bool applyBrake = false;

	void Awake()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		var guns = GetComponentsInChildren<ConnonController>();
		foreach (var gun in guns)
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
			if (wheel.GetComponent<DirectionalWheel>() != null)
			{
				directionalWheels.Add(wheel);
			}
			Wheels.Add(wheel);
			WheelMeshes.Add(wheel.transform.parent.GetComponentInChildren<MeshRenderer>().gameObject);
		}
	}
	
	void Update () 
	{
		float verticalInput = getVerticalInput();
		float horizontalInput = getHorizontalInput();
		Debug.Log(horizontalInput);

		for (int i = 0; i < Wheels.Count; i++)
		{
			Wheels[i].brakeTorque = 0;
			if (applyBrake)
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

		foreach (var wheel in directionalWheels)
		{
			wheel.steerAngle = horizontalInput * steerAngle;
		}
	}


	float getHorizontalInput()
	{
		if (lastHorizontalInput + horizontalInputDelay < Time.time)
		{
			float currentAngle = Vector3.Angle(transform.forward, target.transform.position);
			//Debug.Log(currentAngle);
			if (currentAngle > 2f)
			{
				if (direction < 0.01f && direction > 0.01f)
					direction = 1;
				if (lastAngle > currentAngle)
					direction = -direction;
				lastAngle = currentAngle;
				return 0.5f;
			}
			else
			{
				lastHorizontalInput = Time.time;
				direction = 0;
			}
		}
		return 0;
	}

	float getVerticalInput()
	{
		if(Vector3.Distance(transform.position, target.transform.position) > 20)
		{
			applyBrake = false;
			return 1;
		}
		applyBrake = true;
		return 0;
	}
}
