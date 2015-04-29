using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour 
{
	[SerializeField]
	float speed = 5f;
	[SerializeField]
	float lifeSpan = 10f;
	float creationTime;
	float enableColliderDelay = 0.5f;

	void Start ()
	{
		Destroy(gameObject, lifeSpan);
		creationTime = Time.time;
	}
	
	void Update () 
	{
		transform.position += transform.forward * Time.deltaTime * speed;
		if(creationTime + enableColliderDelay > Time.time)
		{
			GetComponent<CapsuleCollider>().enabled = true;
		}

	}

	void OnCollisionEnter(Collision col)
	{
		Destroy(gameObject);
	}
}
