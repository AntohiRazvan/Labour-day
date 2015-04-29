using UnityEngine;
using System.Collections;

public class EventManager
{
	public delegate void GameEvent();
	public static event GameEvent GameOver;

	public static void TriggerGameOver()
	{
		if (GameOver != null)
		{
			GameOver();
		}
	}
}
