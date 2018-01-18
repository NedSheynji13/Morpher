using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStart : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
              
                

                break;
        }
    }
}
