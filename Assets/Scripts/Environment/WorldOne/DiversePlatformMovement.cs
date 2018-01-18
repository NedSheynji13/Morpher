using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiversePlatformMovement : MonoBehaviour {

	public Vector3 side2Side;
	public Transform A;
	public Transform B;
	public Transform A2;
	public Transform B2;

	public GameObject MoveIt;
	public bool moving;
	public bool nextLevel;

    public void SetTrue ()
    {
        moving = true;
    }


    void Start ()
	{
		nextLevel = false;
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
		if (nextLevel) {
			if (moving) 
			{
				transform.position = Vector3.Lerp(transform.position, B2.position, Time.deltaTime);
				MoveIt.transform.localPosition = new Vector3 ( 0,0.5f,0);

			}
			if (!moving) 
			{
				transform.position = Vector3.Lerp (transform.position, A2.position, Time.deltaTime);
				MoveIt.transform.localPosition = new Vector3 ( 0,0.5f,0);

			}
		}
		if (!nextLevel) {
			if (moving) 
			{
				transform.position = Vector3.Lerp(transform.position, B.position, Time.deltaTime);
				MoveIt.transform.localPosition = new Vector3 ( 0,0.5f,0);

			}
			if (!moving) 
			{
				transform.position = Vector3.Lerp (transform.position, A.position, Time.deltaTime);
				MoveIt.transform.localPosition = new Vector3 ( 0,0.5f,0);

			}
		}

	}
	void Zurück ()
	{
		moving = false;
	}

}
