using UnityEngine;
using System.Collections;

public class AICannonController : MonoBehaviour 
{
	[SerializeField]
	float turnRate = 1;
	[SerializeField]
	GameObject laserBeamPrefab;
	float fireRate = 1.5f;
	float lastFired = 0;
	bool targetDead = false;
	float deathTime = 0;
	GameObject target;
	void Awake ()
	{
		target = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if(targetDead && deathTime + 2.0f < Time.time)
		{
			Application.LoadLevel("menu");
		}
		if (target)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * turnRate);
			if (Vector3.Distance(transform.position, target.transform.position) < 150 && (lastFired + fireRate < Time.time))
			{
				if (!Physics.Raycast(transform.position, transform.forward, 3f))
				{
					lastFired = Time.time;
					Instantiate(laserBeamPrefab, transform.position + 2f * transform.forward, transform.rotation);
				}
			}
		}
		else
		{
			if(!targetDead)
			{
				targetDead = true;
				deathTime = Time.time;
			}
		}
	}
}
