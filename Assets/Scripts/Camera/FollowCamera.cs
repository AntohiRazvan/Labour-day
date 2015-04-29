using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[DisallowMultipleComponent]
class FollowCamera : MonoBehaviour
{
	public ITargetSmoothingAlgorithm targetSmoothingAlgorithm = null;
	public VolumeBoundedCamera volumeBoundedCameraComponent;

	[Range(1.0f, 10.0f)]
	public float sensitivity = 0.0f;

	void Awake()
	{
		targetSmoothingAlgorithm = GetComponent<ITargetSmoothingAlgorithm>();
		volumeBoundedCameraComponent = GetComponent<VolumeBoundedCamera>();
	}

	public void Update()
	{
		var orbitVelocity = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		var offset = volumeBoundedCameraComponent.Offset;

		var radius = offset.magnitude;
		var theta = Mathf.Acos(offset.y / radius);
		var phi = Mathf.Atan2(offset.z, offset.x);

		theta += orbitVelocity.y * Mathf.PI * sensitivity * 1e-2f;
		phi += -orbitVelocity.x * Mathf.PI * sensitivity * 1e-2f;

		theta = Mathf.Clamp(theta, Mathf.PI / 12.0f, Mathf.PI * 11.0f / 12.0f);

		var x = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
		var z = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
		var y = radius * Mathf.Cos(theta);

		volumeBoundedCameraComponent.Offset = new Vector3(x, y, z);
	}
}