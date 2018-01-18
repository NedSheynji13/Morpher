using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour {

	public Vector3 side2Side;
	public Transform A;
	public Transform B;


	public GameObject MoveIt;
	bool moving;

	void Start ()
	{
			moving = false;
	}


	void OnCollisionEnter (Collision other)
	{
		
		switch (other.gameObject.tag)
		{
		case "Basic":
			break;
		case "Player":

			moving = true;
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
			Invoke ("Zurück", 5);
			break;
		}
	}

	void Update ()
	{
		if (moving) 
		{
			transform.position = Vector3.Lerp(transform.position, B.position, Time.deltaTime);

		}
		if (!moving) 
		{
			transform.position = Vector3.Lerp (transform.position, A.position, Time.deltaTime);

		}
	}
	void Zurück ()
	{
		moving = false;
	}

}
