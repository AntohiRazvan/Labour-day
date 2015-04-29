using UnityEngine;
using System.Collections;

public class VehicleHealth : MonoBehaviour 
{
	[SerializeField]
	float ammount;
	bool gameOver = false;

	void OnLevelWasLoaded()
	{
		ammount = GetComponentsInChildren<MeshRenderer>().Length*25;
	}

	void Awake()
	{
		ammount = GetComponentsInChildren<MeshRenderer>().Length*25;
	}

	void Update()
	{
		if (ammount < 0)
		{
			Instantiate(Resources.Load("Prefabs/Explosion"), transform.position, Quaternion.identity);

			if (gameObject.tag == "Player" && !gameOver)
			{
				gameOver = true;
				EventManager.TriggerGameOver();
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

	public void takeDmage(int damage)
	{
		ammount -= damage;
	}
}
