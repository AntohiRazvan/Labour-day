using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	[SerializeField]
	int ammount;
	VehicleHealth vehicleHealth;
	
	void Awake()
	{
		if (transform.root.name == "Boss")
			Debug.Log(transform.root.name);
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
