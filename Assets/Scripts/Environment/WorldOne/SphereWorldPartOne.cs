using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWorldPartOne : MonoBehaviour {

    public GameObject SphereWorld;
	
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                SphereWorld.GetComponent<SphereWorldProgress>().done();



                break;
        }
    }

}
