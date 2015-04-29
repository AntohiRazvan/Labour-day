using UnityEngine;
using System.Collections;

public class ConnonController : MonoBehaviour 
{
	[SerializeField]
	GameObject targetCursor;
	[SerializeField]
	float turnRate = 1;
	[SerializeField]
	GameObject laserBeamPrefab;
	[SerializeField]
	float fireRate = 0.2f;

	Vector3 pos;
	float lastFired = 0;

	void Start () 
	{
		targetCursor = GameObject.FindGameObjectWithTag("Cursor");
	}
	
	void Update () 
	{
		//Vector3 forwardVector = Camera.main.ScreenToWorldPoint(targetCursor.transform.forward).normalized;
		//Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(targetCursor.transform.position);
		//pos = cursorPosition - forwardVector * (Vector3.Distance(cursorPosition, transform.position) + 5);
		//Debug.Log(forwardVector);
		Ray ray = Camera.main.ScreenPointToRay(targetCursor.transform.position);
		RaycastHit hitInfo;
		Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ((1 << 8) | (1 << 9)));
		Vector3 target = hitInfo.point;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), Time.deltaTime*turnRate);

		if(Input.GetMouseButton(0) && (lastFired + fireRate < Time.time))
		{
			if (!Physics.Raycast(transform.position, transform.forward, 3f))
			{
				lastFired = Time.time;
				Instantiate(laserBeamPrefab, transform.position + 2f * transform.forward, transform.rotation);
			}
		}
	}
}
