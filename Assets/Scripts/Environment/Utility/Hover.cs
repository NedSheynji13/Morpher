using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    #region Variables
    private Vector3 basePosition;
	#endregion
	
	void Start ()
	{
        basePosition = transform.position;
    }
	
	void Update () 
	{
            transform.Rotate(0, 2, 0);
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * 0.1f + basePosition.y, transform.position.z);
    }
}
