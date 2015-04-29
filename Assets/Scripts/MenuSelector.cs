using UnityEngine;
using System.Collections;

public class MenuSelector : MonoBehaviour {

	public AudioClip selectSound;
	void Start () 
	{
	
	}
	

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.DownArrow) && gameObject.transform.position.y > -1.5f)
		{
			AudioSource.PlayClipAtPoint(selectSound, transform.position);
			gameObject.transform.Translate(0f, -1.5f, 0f);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.transform.position.y < 1.5f)
		{
			AudioSource.PlayClipAtPoint(selectSound, transform.position);
			gameObject.transform.Translate(0f, 1.5f, 0f);
		}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(gameObject.transform.position.y == 1.5f)
			{
				Application.LoadLevel("vehicle_creation");
			}
			else if(gameObject.transform.position.y == 0f)
			{
				Application.LoadLevel("instructions");
			}
			else if(gameObject.transform.position.y == -1.5f)
			{
				Application.Quit();
			}
		}
	}
}
