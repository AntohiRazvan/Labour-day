using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface VehicleController
{
	List<WheelCollider> directionalWheels {get; set;}
	List<WheelCollider> Wheels{get; set;}
	List<GameObject> WheelMeshes{get; set;}

	void RemoveWheel(GameObject wheel);
}
