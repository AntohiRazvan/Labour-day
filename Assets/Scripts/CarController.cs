﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour, VehicleController
{
	[SerializeField]
	float motorTorque;
	[SerializeField]
	float steerAngle;
	[SerializeField]
	float brakeTorque;
	[SerializeField]
	GameObject explosionPrefab;

	Rigidbody rigidbody;
	public List<WheelCollider> directionalWheels { get; set;}
	public List<WheelCollider> Wheels { get; set; }
	public List<GameObject> WheelMeshes { get; set; }

	void Start () 
	{
		EventManager.GameOver += OnGameOver;
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
			if (WheelMeshes[i] != null)
			{
				Wheels[i].brakeTorque = 0;
				if (Input.GetKey(KeyCode.Space))
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
		}

		foreach(var wheel in directionalWheels)
		{
			if(wheel != null)
				wheel.steerAngle = horizontalInput * steerAngle;
		}
		
	}

	public void RemoveWheel(GameObject wheel)
	{
		WheelCollider wheelCollider = wheel.GetComponentInChildren<WheelCollider>();
		Wheels.Remove(wheelCollider);
		directionalWheels.Remove(wheelCollider);
		WheelMeshes.Remove(wheel);
	}

	void OnGameOver()
	{
		StartCoroutine(Pause(2));
		Destroy(gameObject, 0.2f);

	//	Pause(2);


	}

	private IEnumerator Pause(int p)
	{
		yield return new WaitForSeconds(p);
		Application.LoadLevel("menu");
	}
}
