using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWorldProgress : MonoBehaviour {

    public Material StartMat;
    public Material DoneMat;
    public GameObject SphereWorld;


    void Start ()
    {
        GetComponent<Renderer>().material = StartMat;

    }
   public  void done ()
    {
        GetComponent<Renderer>().material = DoneMat;
        SphereWorld.GetComponent<SphereWorld>().PartDone();
    }



    void Update ()
    {
	    	
	}
}
