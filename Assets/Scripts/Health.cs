using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	[SerializeField]
	int ammount;
	VehicleHealth vehicleHealth;
	
	void Awake()
	{
		vehicleHealth = transform.root.GetComponent<VehicleHealth>();
	}

	void Update () 
	{
		if (ammount <= 0)
			Destroy(gameObject);
	}

	public void takeDmage(int damage)
	{
		ammount -= damage;
		vehicleHealth.takeDmage(damage);
	}
}
