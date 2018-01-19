using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Affector : MonoBehaviour
{
    #region Variables
    public enum Options {MoveUp, MoveDown, MoveRight, MoveLeft};
    public Options React;
    public float destroyAfter;
    [HideInInspector] public bool affected = false;
	#endregion
	
	void Update () 
	{
        if (affected)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            switch (React)
            {
                case Options.MoveUp:
                    {
                        transform.Translate(transform.rotation * Vector3.up * 0.1f);
                        break;
                    }
                case Options.MoveDown:
                    {
                        transform.Translate(transform.rotation * Vector3.down * 0.1f);
                        break;
                    }
                case Options.MoveRight:
                    {
                        transform.Translate(transform.rotation * Vector3.back * 0.1f);
                        break;
                    }
                case Options.MoveLeft:
                    {
                        transform.Translate(transform.rotation * Vector3.forward * 0.1f);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            StartCoroutine(Destroy(destroyAfter));
        }
	}

    private IEnumerator Destroy(float _time)
    {
        yield return new WaitForSeconds(_time);
        DestroyImmediate(gameObject);
    }
}
