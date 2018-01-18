using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWay : MonoBehaviour {

    public Material StartMat;
    public Material PressedMat;
    public GameObject Anfang;

    Vector3 Go;
    public bool CorrectField ;

    void Start () {
        Go = Anfang.transform.position;
        GetComponent<Renderer>().material = StartMat;

    }

    void OnCollisionStay(Collision other)
    {

        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                    if (CorrectField)
                      {
                    GetComponent<Renderer>().material = PressedMat;
                      }
                     if (!CorrectField)
                     {
                    other.gameObject.transform.position = Go;
                        }


                break;

        }
    }
    


    void Update ()
    {
		
	}

    }