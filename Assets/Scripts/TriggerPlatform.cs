using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour {


    public GameObject Moveable;


    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                Moveable.GetComponent<DiversePlatformMovement>().nextLevel = true;


                break;
        }
    }
}
