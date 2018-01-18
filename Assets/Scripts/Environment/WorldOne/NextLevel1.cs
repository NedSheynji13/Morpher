using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel1 : MonoBehaviour {

	public GameObject Moveable;
	public bool isPressed;
	private Vector3 startPos;
	private Vector3 pressedPosition;

	void Start () 
	{
		startPos = transform.position;
		pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

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
                    Moveable.GetComponent<DiversePlatformMovement>().nextLevel = true;
				}
			break;
		}
	}

    void OnCollisionExit(Collision other)
    {

        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                if (isPressed)
                {
                    isPressed = false;
                }
                break;
        }
    }

    void FixedUpdate()
	{	
		if (isPressed) 
		{
			transform.position = Vector3.Lerp(transform.position, pressedPosition, Time.deltaTime);
		}
		if (!isPressed) 
		{
			transform.position = Vector3.Lerp (transform.position,startPos,0.2f);
		}
	}
}
