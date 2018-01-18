using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bewegung : MonoBehaviour 
{
	public bool isPressed;

	public GameObject MoveIt;
	public Material StartMat;
	public Material	PressedCorrectly;

	private Vector3 Pos;
	private Vector3 startPos;
	private Vector3 pressedPosition;
	private Vector3 Hoch;
	private Vector3 Runter;
	private Vector3 Links;
	private Vector3 Rechts;

	void Start()
	{
		GetComponent<Renderer> ().material = StartMat;

		Hoch = new Vector3(MoveIt.transform.position.x,MoveIt. transform.position.y+2,MoveIt.transform.position.z);
		Runter = new Vector3(MoveIt.transform.position.x,MoveIt. transform.position.y-20,MoveIt.transform.position.z);
		Links = new Vector3(MoveIt.transform.position.x,MoveIt.transform.position.y,MoveIt. transform.position.z+2);
		Rechts = new Vector3(MoveIt.transform.position.x, MoveIt.transform.position.y,MoveIt. transform.position.z-2);
		pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.24f, transform.position.z);
		startPos = transform.position;
		Pos = MoveIt.transform.position;

		isPressed = false;
	}

	void FixedUpdate()
	{	
		if (isPressed) 
		{
			transform.position = Vector3.Lerp(transform.position, pressedPosition, Time.deltaTime);
			MoveIt.transform.position = Vector3.Lerp(MoveIt.transform.position, Runter, Time.deltaTime);
		}
		if (!isPressed) 
		{
			GetComponent<Renderer> ().material = StartMat;
			transform.position = Vector3.Lerp (transform.position,startPos,0.2f);
			MoveIt.transform.position = Vector3.Lerp(MoveIt.transform.position,Pos, Time.deltaTime);
		}
	}

	void OnCollisionStay (Collision other)
	{
		switch (other.gameObject.tag)
		{
		case "Basic":
			break;

		case "Player":
			if (isPressed == false)
			{
				isPressed = true;
				GetComponentInParent<SwitchConsoleScript> ().Count(1);
				GetComponent<Renderer> ().material = PressedCorrectly;
			}
			break;
		}
	}

	void OnCollisionExit (Collision other)
	{
		switch (other.gameObject.tag)
		{
		case "Basic":
			break;
		case "Player":
			GetComponent<Renderer> ().material = StartMat;
			isPressed = false;
			break;
		}
	}
}