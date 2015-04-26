using UnityEngine;
using System.Collections;

public class BlockPlacer : MonoBehaviour 
{
	[SerializeField]
	GameObject blockPrefab;
	[SerializeField]
	GameObject wheelPrefab;
	GameObject currentObject;
	GameObject vehicle;
	float blockSize;

	void Start () 
	{
		vehicle = GameObject.FindGameObjectWithTag("Vehicle");
		currentObject = wheelPrefab;
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
				if (hitInfo.collider.gameObject.tag == "Block")
				{
					Vector3 buildPosition = getBuildPosition(hitInfo.collider.gameObject.transform.position, hitInfo.point);
					if (buildPosition != Vector3.zero)
					{
						GameObject go = (GameObject)Instantiate(currentObject, buildPosition, Quaternion.identity);
						go.transform.parent = vehicle.transform;
						if (currentObject.name == "Wheel")
						{
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
		if (Physics.OverlapSphere(ret, 0.2f).Length > 0)
			return Vector3.zero;
		return ret;
	}
}
