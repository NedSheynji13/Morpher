using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Affector : MonoBehaviour
{
    #region Variables
    [HideInInspector] public bool affected = false;
	#endregion
	
	void Update () 
	{
        if (affected) transform.GetComponent<IMove>().Move(2.0f);
	}
}

public interface IMove
{
    void Move(object o);
}


