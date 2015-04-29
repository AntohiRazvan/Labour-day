using UnityEngine;
using System.Collections;

public class BlockPlacer : MonoBehaviour 
{
	[SerializeField]
	GameObject blockPrefab;
	[SerializeField]
	GameObject directionalWheelPrefab;
	[SerializeField]
	GameObject staticWheelPrefab;
	[SerializeField]
	GameObject cannonPrefab;
	[SerializeField]
	GameObject lookAtPosition;
	GameObject currentObject;
	GameObject vehicle;
	float blockSize;

	void Start () 
	{
		vehicle = GameObject.FindGameObjectWithTag("Player");
		currentObject = blockPrefab;
		blockSize = blockPrefab.transform.localScale.x;
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (currentObject == null)
				{
					if (hitInfo.collider.gameObject.name != "Root" &&
						 (hitInfo.collider.gameObject.tag == "Block" || 
						  hitInfo.collider.gameObject.tag == "Wheel" || 
						  hitInfo.collider.gameObject.tag == "Gun"))
					{
						Destroy(hitInfo.collider.gameObject);
					}
				}
				else
				{
					if (hitInfo.collider.gameObject.tag == "Block")
					{
						Vector3 buildPosition = getBuildPosition(hitInfo.collider.gameObject.transform.position, hitInfo.point);
						if (buildPosition != Vector3.zero)
						{
							GameObject go = (GameObject)Instantiate(currentObject, buildPosition, Quaternion.identity);
							go.transform.parent = vehicle.transform;
							if (currentObject.name.ToLower().Contains("wheel") || currentObject.name.ToLower().Contains("gun"))
							{
								go.transform.LookAt(hitInfo.collider.gameObject.transform);
							}
						}
					}
				}
			}
		}
	}

	Vector3 getBuildPosition(Vector3 objectPosition, Vector3 hitPosition)
	{
		float distance, minDistance = 100;
		Vector3 ret = Vector3.zero;
		distance = Vector3.Distance(hitPosition, objectPosition + new Vector3(blockSize, 0, 0));
		if (distance < minDistance)
		{
			minDistance = distance;
			ret = objectPosition + new Vector3(blockSize, 0, 0);
		}
		distance = Vector3.Distance(hitPosition, objectPosition + new Vector3(-blockSize, 0, 0));
		if (distance < minDistance)
		{
			minDistance = distance;
			ret = objectPosition + new Vector3(-blockSize, 0, 0);
		}
		distance = Vector3.Distance(hitPosition, objectPosition + new Vector3(0, blockSize, 0));
		if (distance < minDistance)
		{
			minDistance = distance;
			ret = objectPosition + new Vector3(0, blockSize, 0);
		}
		distance = Vector3.Distance(hitPosition, objectPosition + new Vector3(0, -blockSize, 0));
		if (distance < minDistance)
		{
			minDistance = distance;
			ret = objectPosition + new Vector3(0, -blockSize, 0);
		}
		distance = Vector3.Distance(hitPosition, objectPosition + new Vector3(0, 0, blockSize));
		if (distance < minDistance)
		{
			minDistance = distance;
			ret = objectPosition + new Vector3(0, 0, blockSize);
		}
		distance = Vector3.Distance(hitPosition, objectPosition + new Vector3(0, 0, -blockSize));
		if (distance < minDistance)
		{
			minDistance = distance;
			ret = objectPosition + new Vector3(0, 0, -blockSize);
		}
		if (currentObject == directionalWheelPrefab || currentObject == staticWheelPrefab)
		{
			if (Physics.OverlapSphere(ret, 0.35f).Length > 1)
				return Vector3.zero;
		}
		else 
		{
			if (Physics.OverlapSphere(ret, 0.24f).Length > 0)
				return Vector3.zero;
		}

		return ret;
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 150, 100), "Block"))
		{
			currentObject = blockPrefab;
		}
		if (GUI.Button(new Rect(0, 100, 150, 100), "Static Wheel"))
		{
			currentObject = staticWheelPrefab;
		}
		if (GUI.Button(new Rect(0, 200, 150, 100), "Directional Wheel"))
		{
			currentObject = directionalWheelPrefab;
		}
		if (GUI.Button(new Rect(0, 300, 150, 100), "Beer Cannon"))
		{
			currentObject = cannonPrefab;
		}
		if (GUI.Button(new Rect(0, 400, 150, 100), "Remove Component"))
		{
			currentObject = null;
		}
		if (GUI.Button(new Rect(0, 500, 150, 100), "Ready!"))
		{
			DontDestroyOnLoad(vehicle);
			Application.LoadLevel("singleplayer");
		}
	}
}
