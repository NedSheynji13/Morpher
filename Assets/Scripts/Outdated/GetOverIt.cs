using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOverIt: MonoBehaviour 
{
	public bool isPressed;

	public GameObject MoveIt;

	private Vector3 Pos;
	private Vector3 startPos;
	private Vector3 pressedPosition;
	private Vector3 Hoch;
	private Vector3 Runter;
	private Vector3 Links;
	private Vector3 Rechts;

	void Start()
	{

		Hoch = new Vector3(MoveIt.transform.position.x,MoveIt. transform.position.y+2,MoveIt. transform.position.z);
		Runter = new Vector3(MoveIt.transform.position.x,MoveIt. transform.position.y- 10, MoveIt.transform.position.z);
		Links = new Vector3(MoveIt.transform.position.x,MoveIt.transform.position.y,MoveIt. transform.position.z+2);
		Rechts = new Vector3(MoveIt.transform.position.x, MoveIt.transform.position.y,MoveIt. transform.position.z-2);
		pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.12f, transform.position.z);
		startPos = transform.position;
		Pos = MoveIt.transform.position;

		isPressed = false;
	}
	void Reset()
	{
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
			Invoke ("Reset", 5);
			break;
		}
	}
}