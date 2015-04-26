using UnityEngine;
using System.Collections;

public class VehicleCreationCamera : MonoBehaviour 
{
	[SerializeField]
	float sensitivityX = 15;

	[SerializeField]
	float sensitivityY = 15;
	
	[SerializeField]
	float minimumX = -360;
	
	[SerializeField]
	float maximumX = 360;
	
	[SerializeField]
	float minimumY = -60;
	
	[SerializeField]
	float maximumY = 60;
	
	[SerializeField]
	float moveSpeed = 10;
	
	float rotationX = 0;
	float rotationY = 0;

	Rigidbody rigidbody;

	void Start () 
	{
		Cursor.visible = true;
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
		if (Input.GetMouseButton(1)) 
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			rotationY = ClampAngle(rotationY, minimumY, maximumY);

			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

			transform.localRotation = xQuaternion * yQuaternion;
		}

		var vAxis = Input.GetAxis("Vertical");
		var hAxis = Input.GetAxis("Horizontal");
		rigidbody.AddForce(transform.forward * vAxis * Time.deltaTime * moveSpeed, ForceMode.VelocityChange);
		rigidbody.AddForce(transform.right * hAxis * Time.deltaTime * moveSpeed, ForceMode.VelocityChange);
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
