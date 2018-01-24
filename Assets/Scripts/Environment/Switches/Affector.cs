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
        if (affected)
        {
            try { transform.GetComponent<IMove>().Move(); }
            catch { affected = false; }
        }
	}
}

public interface IMove
{
    void Move();
}


