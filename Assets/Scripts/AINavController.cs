using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AIMoveMode
{
	public string name;
	public float moveSpeedFactor;
}

public class AINavController : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;
	public Transform targetTest;
	public float moveSpeedFactor;


	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();

		// Don't let the nav agent update our character position or rotation.
		navMeshAgent.updatePosition = false;
		navMeshAgent.updateRotation = false;
	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		//if (targetTest != null)
		{
			// Set the agent current destination (not wise to set it repeatedlly as it will trigger repeated path calculations).
			//navMeshAgent.SetDestination(targetTest.position);
			// Manually control the agent and set him to the position our character is currently at.
			navMeshAgent.nextPosition = transform.position;
		}
	}

	public Vector3 GetCharacterInputVelocity()
	{
		// Use the navigation agent current velocity vector as the input for anyone using this ICharacterInputProvider (like RootMotionLocomotion).
		return Vector3.ClampMagnitude(navMeshAgent.desiredVelocity, moveSpeedFactor);
	}

}