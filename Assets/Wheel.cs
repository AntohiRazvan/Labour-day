using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour 
{
	VehicleController vehicleController;
	void Awake()
	{
		vehicleController = transform.parent.gameObject.GetComponent<VehicleController>();
	}

	void OnDestroy()
	{
		//vehicleController.RemoveWheel(gameObject);
	}
}
