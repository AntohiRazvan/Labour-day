using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
	public int initialSpawn;
	public int maxX;
	public int maxY;
	public float spawnRate;

	private float radius = 3;
	private double lastSpawn;

	void Start () 
	{
		lastSpawn = Time.time;
		for (int i = 0; i < initialSpawn; i++)
		{
			SpawnEgg();
		}
	}
	
	void Update () 
	{
		if(lastSpawn + spawnRate < Time.time)
		{
			lastSpawn = Time.time;
			SpawnEgg();
		}
	}

	void SpawnEgg()
	{
		int x = Random.Range(-maxX, maxX);
		int y = Random.Range(-maxY, maxY);
		Vector3 spawnPosition = new Vector3(x, 0f, y);
		Collider[] colliders = Physics.OverlapSphere (spawnPosition, radius);
		if(colliders.Length < 2)
		{
			Instantiate(Resources.Load("Prefabs/Egg"), spawnPosition, Quaternion.identity);
		}
	}
}
