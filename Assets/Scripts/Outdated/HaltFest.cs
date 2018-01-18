using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaltFest : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{

		switch (other.gameObject.tag)
		{
		case "Basic":
			break;
		case "Player":
			other.gameObject.GetComponentInParent<Transform>().transform.parent = gameObject.transform;	

			break;
		}
	}
	void OnTriggerExit (Collider other)
	{
		switch (other.gameObject.tag)
		{
		case "Basic":
			break;
		case "Player":
			other.transform.parent = null;
			break;
		}
	}




	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
