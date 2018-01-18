using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchConsoleScript : MonoBehaviour {

	public GameObject MoveIt;

	public int CorrectInputs;

	public Component[] Scripts;

	private Vector3 Hoch;
	private Vector3 Runter;
	private Vector3 Links;
	private Vector3 Rechts;

	void Start()
	{
		Hoch = new Vector3(MoveIt.transform.position.x,MoveIt.transform.position.y+2, MoveIt.transform.position.z);
		Runter = new Vector3(MoveIt.transform.position.x,MoveIt.transform.position.y-2, MoveIt.transform.position.z);
		Links = new Vector3(MoveIt.transform.position.x,MoveIt.transform.position.y, MoveIt.transform.position.z+2);
		Rechts = new Vector3(MoveIt.transform.position.x, MoveIt.transform.position.y-2, MoveIt.transform.position.z-2);

		Scripts = GetComponentsInChildren<GreenButtonScript>();

		foreach (GreenButtonScript wert in Scripts) 
		{
			if (wert.isPressed == true) 
			{
				CorrectInputs++;
			}
		}
	}

	public void Reset()
	{	
		CorrectInputs = 0;
		foreach (GreenButtonScript wert in Scripts) 
		{
			wert.isPressed = false;
		}
	}

	public void Count (int plus)
	{
		if (CorrectInputs < 3)
		{
			CorrectInputs = CorrectInputs + plus;
			Debug.Log ("okay");
		}
	}

	void Update () 
	{
		if (CorrectInputs == 3) 
		{
			MoveIt.transform.position = Vector3.Lerp(MoveIt.transform.position, Hoch, Time.deltaTime);
		}
	}
}
