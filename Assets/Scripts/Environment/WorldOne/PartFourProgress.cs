using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartFourProgress : MonoBehaviour
{

    public GameObject End;


    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                End.GetComponent<PartFourScript>().PartDone();

                break;
        }
    }

}
