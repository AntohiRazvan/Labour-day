using UnityEngine;
using System.Collections;

public class InstructionsMenu : MonoBehaviour 
{
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("menu");
		}
	}
}
