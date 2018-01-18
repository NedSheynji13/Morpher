using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenButtonScript : MonoBehaviour 
{
	public bool isPressed;

	private Vector3 Pos;
	private Vector3 startPos;
	private Vector3 pressedPosition;

	public Material StartMat;
	public Material	PressedCorrectly;

	void Start()
	{
		pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
		startPos = transform.position;
		GetComponent<Renderer> ().material = StartMat;
		Pos = transform.position;
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
				GetComponent<Renderer> ().material = PressedCorrectly;

				GetComponentInParent<SwitchConsoleScript> ().Count(1);
			}
			break;
		}
	}

}