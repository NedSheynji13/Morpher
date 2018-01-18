using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonScript : MonoBehaviour 
{
	public bool isPressed;

	public Material StartMat;
	public Material PressedWrong;


	private Vector3 startPos;
	private Vector3 Pos;
	private Vector3 pressedPosition;

	void Start ()
	{
		GetComponent<Renderer> ().material = StartMat;
		pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.24f, transform.position.z);
		Pos = transform.position;
		startPos = transform.position;
		isPressed = false;	
	}
	void Reset ()
	{
		GetComponent<Renderer> ().material = StartMat;
		isPressed = false;

	}

	void FixedUpdate()
	{	
		if (isPressed) 
		{
			transform.position = Vector3.Lerp(transform.position, pressedPosition, Time.deltaTime);
		}
		if (!isPressed) 
		{
			GetComponent<Renderer> ().material = StartMat;
			transform.position = Vector3.Lerp (transform.position,startPos,0.2f);
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
				GetComponentInParent<SwitchConsoleScript> ().Reset();
				GetComponent<Renderer> ().material = PressedWrong;
			}
			break;
		case"Spring":
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
		case"Spring":
			break;
		}
	}
}
