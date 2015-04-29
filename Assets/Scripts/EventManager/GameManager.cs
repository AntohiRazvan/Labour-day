using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	[SerializeField]
	GameObject AIPrefab;
	[SerializeField]
	GameObject BossPrefab;
	[SerializeField]
	float spawnInterval;
	[SerializeField]
	float bossSpawn;
	float lastSpawn = 0;
	bool bossSpawned = false;
	
	void Update () 
	{
		if(lastSpawn + spawnInterval < Time.time)
		{
			lastSpawn = Time.time;
			Instantiate(AIPrefab, GetSpawnPosition(), Quaternion.identity);
		}
		if(!bossSpawned && Time.time > bossSpawn)
		{
			bossSpawned = true;
			Instantiate(BossPrefab, Vector3.zero, Quaternion.identity);
		}
	}

	public static Vector3 GetSpawnPosition()
	{
tryAgain:
		float x = Random.Range(-200f, 200f);
		float y = Random.Range(-200f, 200f);
		Vector3 spawnPosition = new Vector3(x, 1, y);
		Collider[] colliders = Physics.OverlapSphere(spawnPosition, 10);
		if (colliders.Length > 1)
			goto tryAgain;
		return spawnPosition;
	}
}
